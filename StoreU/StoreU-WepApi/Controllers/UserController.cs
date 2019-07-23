using System; 
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
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

                await _repository.AddUser(userEntity, userDto.PasswordRaw);
                //Returning the new resource/Dto
                var userToReturnDto = _mapper.Map<UserDto>(userEntity);
                var tokenResult = _repository.GenerateToken(userToReturnDto.UserId, userToReturnDto.RoleId.Value, _configuration["AppSettings:Secret"]);

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

            var tokenResult = _repository.GenerateToken(userEntity.UserId, userEntity.RoleId.Value, _configuration["AppSettings:Secret"]);
            var userToReturn = _mapper.Map<UserDto>(userEntity);
            userToReturn.Token = tokenResult.Token;
            userToReturn.TokenExpiration = tokenResult.TokenExpiration;
            _logger.LogInformation($"{userToReturn.FullName} has been successfully logged in");
            return Ok(userToReturn);
        }

        [AllowAnonymous]
        [HttpGet("GenerateCode")]
        public async Task<IActionResult> GenerateCode(string email)
        {
            try
            {
               var userResponse = await _repository.GenerateCode(email);

                string message = $"Código enviado al correo {userResponse.Email}";
                _logger.LogInformation(message);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResponseDto(ex.Message, false));
            }
        }

        [AllowAnonymous]
        [HttpGet("ValidateCode")]
        public async Task<IActionResult> ValidateCode(string email, int codeNumber)
        {
            try
            {
                await _repository.ValidateCode(email, codeNumber);
                string message = $"Código validado exitosamente {email}/{codeNumber}";
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResponseDto(ex.Message, false));
            }
        }

        [AllowAnonymous]
        [HttpPost("SetPassword")]
        public async Task<IActionResult> SetPassword([FromBody] UserChangePasswordDto model)
        {
            try
            {
                await _repository.SetPassword(model);
                string message = $"Password modificado exitosamente.";
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResponseDto(ex.Message, false));
            }
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
    }
}