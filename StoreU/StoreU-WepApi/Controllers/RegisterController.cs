using AutoMapper;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StoreU_WepApi.Services.Register;

namespace StoreU_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegisterController : ControllerBase
    {
        private IMapper _mapper;
        private readonly ILogger<RegisterController> _logger;
        private IConfiguration _configuration;
        private IRegisterRepository _repository;

        public RegisterController(IMapper mapper, IConfiguration configuration, ILogger<RegisterController> logger, IRegisterRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
        }


    }
}