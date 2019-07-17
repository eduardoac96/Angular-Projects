using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StoreU_DomainEntities;
using StoreU_DomainEntities.Users;
using StoreU_WebApi.Model;
using StoreU_WebApi.Services.Users;

namespace StoreU_WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUsersRepository _repository;
        private IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        private IConfiguration _configuration;

        public UserController(IMapper mapper, IConfiguration configuration, ILogger<UserController> logger, IUsersRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserDto userDto)
        {
            try
            {
                var userEntity = _mapper.Map<Users>(userDto);
                userEntity.UserId = Guid.NewGuid(); 
                userEntity.RegistryDate = DateTime.Now;

                _repository.AddUser(userEntity, userDto.PasswordRaw);
                if (!(await _repository.SaveAsync()))
                {
                    throw new Exception("Adding a user failed on save.");
                }

                //Returning the new resource/Dto
                var userToReturnDto = _mapper.Map<UserDto>(userEntity);
                var tokenResult = GenerateToken(userToReturnDto.UserId, userToReturnDto.RoleId.Value);
                userToReturnDto.Token = tokenResult.Token;
                userToReturnDto.TokenExpiration = tokenResult.TokenExpiration;
                _logger.LogInformation($"A new user has been registered: Name = {userToReturnDto.FullName}, Username = {userToReturnDto.UserName}, Role = {userToReturnDto.RoleId}");
                return CreatedAtRoute("GetUser", new { userId = userToReturnDto.UserId }, userToReturnDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResponseDto(ex.Message, false));

            }

        }
        [AllowAnonymous]
        [HttpGet("authenticate")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var userEntity = await _repository.VerifyCredentials(username, password);
            if (userEntity == null)
            {
                return NotFound(new ActionResponseDto("Invalid credentials"));
            }

            var tokenResult = GenerateToken(userEntity.UserId, userEntity.RoleId.Value);
            var userToReturn = _mapper.Map<UserDto>(userEntity);
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

        [HttpGet("single", Name = "GetUser")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            if (userId == null || userId == Guid.Empty)
            {
                return BadRequest(new ActionResponseDto("Invalid userId"));
            }
            var repoUser = await _repository.GetUser(userId);
            var dtoUser = _mapper.Map<UserDto>(repoUser);
            return Ok(dtoUser);
        }

        [HttpGet]
        [Authorize(nameof(Constants.PolicyNames.MustBeAdministrator))]
        public async Task<IActionResult> GetUsers()
        {
            var repoUsers = await _repository.GetUsers();
            var dtoUsers = _mapper.Map<IEnumerable<UserDto>>(repoUsers);
            return Ok(dtoUsers);
        }

        [HttpGet("paginated")]
        [Authorize(nameof(Constants.PolicyNames.MustBeAntiqueUser))]
        public async Task<IActionResult> GetUsersPaginated(string sortField, string sortDirection, int maxRecordsPerPage, int pageIndex, string id, string displayName, string username, string role)
        {
            if (maxRecordsPerPage <= 0 || pageIndex < 0)
            {
                return BadRequest(new ActionResponseDto("Invalid parameters for paginated search"));
            }
            var paginatedResult = await _repository.GetUsersPaginated(sortField, sortDirection, maxRecordsPerPage, pageIndex, id, displayName, username, role);
            return Ok(new PaginatedDto<UserDto>
            {
                TotalRecords = paginatedResult.TotalUsers,
                FilteredRecords = _mapper.Map<IEnumerable<UserDto>>(paginatedResult.PaginatedUsers)
            });
        }



    }
}