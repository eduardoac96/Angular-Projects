using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper; 
using LearnU_DomainEntities;
using LearnU_DomainEntities.Users;
using LearnU_WebApi.Constants;
using LearnU_WebApi.Models;
using LearnU_WebApi.Services.User;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LearnU_WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserRepository _repository;
        private IMapper _mapper;

        private readonly ILogger<UserController> _logger;

        private IConfiguration _configuration;
        public UserController(IMapper mapper, IConfiguration configuration, ILogger<UserController> logger, IUserRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserMaintenanceDTO userDto)
        {
            try
            {
                var userEntity = _mapper.Map<Users>(userDto);
                userEntity.UserId = Guid.NewGuid();
                userEntity.RoleId = (int)RoleClaimValues.Student;
                userEntity.RegistryDate = DateTime.Now; 

                _repository.AddUser(userEntity, userDto.PasswordRaw);
                if (!(await _repository.SaveAsync()))
                {
                    throw new Exception("Adding a user failed on save.");
                }

                //Returning the new resource/Dto
                var userToReturnDto = _mapper.Map<UserDisplayDTO>(userEntity);
                var tokenResult = GenerateToken(userToReturnDto.UserId, userToReturnDto.RoleID);
                userToReturnDto.Token = tokenResult.Token;
                userToReturnDto.TokenExpiration = tokenResult.TokenExpiration;
                _logger.LogInformation($"A new user has been registered: Name = {userToReturnDto.FullName}, Username = {userToReturnDto.UserName}, Role = {userToReturnDto.RoleID}");
                return CreatedAtRoute(nameof(Constants.RouteNames.GetUser), new { userId = userToReturnDto.UserId }, userToReturnDto);
            }
            catch (Exception ex)
            { 

                throw ex;
            }
          
        }
        [AllowAnonymous]
        [HttpGet("authenticate")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var userEntity = await _repository.VerifyCredentials(username, password);
            if (userEntity == null)
            {
                return NotFound(new ActionResponseDTO("Invalid credentials"));
            }

            var tokenResult = GenerateToken(userEntity.UserId, userEntity.RoleId.Value);
            var userToReturn = _mapper.Map<UserDisplayDTO>(userEntity);
            userToReturn.Token = tokenResult.Token;
            userToReturn.TokenExpiration = tokenResult.TokenExpiration;
            _logger.LogInformation($"{userToReturn.FullName} has been successfully logged in");
            return Ok(userToReturn);
        }

        private (string Token, string TokenExpiration) GenerateToken(Guid userId, int role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
              
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);

            var expirationDate = DateTime.UtcNow.AddHours(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString()),
                    new Claim(ClaimTypes.Role, role.ToString())
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), new DateTimeOffset(expirationDate, TimeSpan.FromMilliseconds(0)).ToString(Constants.Constants.DATE_TIME_WITH_TIMEZONE_FORMAT));
        }

        [HttpGet("single", Name = nameof(Constants.RouteNames.GetUser))]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            if (userId == null || userId == Guid.Empty)
            {
                return BadRequest(new ActionResponseDTO("Invalid userId"));
            }
            var repoUser = await _repository.GetUser(userId);
            var dtoUser = _mapper.Map<UserDisplayDTO>(repoUser);
            return Ok(dtoUser);
        }

        [HttpGet]
        [Authorize(nameof(Constants.PolicyNames.MustBeAdministrator))]
        public async Task<IActionResult> GetUsers()
        {
            var repoUsers = await _repository.GetUsers();
            var dtoUsers = _mapper.Map<IEnumerable<UserDisplayDTO>>(repoUsers);
            return Ok(dtoUsers);
        }

        [HttpGet("paginated")]
        [Authorize(nameof(Constants.PolicyNames.MustBeAntiqueUser))]
        public async Task<IActionResult> GetUsersPaginated(string sortField, string sortDirection, int maxRecordsPerPage, int pageIndex, string id, string displayName, string username, string role)
        {
            if (maxRecordsPerPage <= 0 || pageIndex < 0)
            {
                return BadRequest(new ActionResponseDTO("Invalid parameters for paginated search"));
            }
            var paginatedResult = await _repository.GetUsersPaginated(sortField, sortDirection, maxRecordsPerPage, pageIndex, id, displayName, username, role);
            return Ok(new PaginatedDto<UserDisplayDTO>
            {
                TotalRecords = paginatedResult.TotalUsers,
                FilteredRecords = _mapper.Map<IEnumerable<UserDisplayDTO>>(paginatedResult.PaginatedUsers)
            });
        }

    }
}