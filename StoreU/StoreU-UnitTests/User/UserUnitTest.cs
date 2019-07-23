using AutoMapper;
using StoreU_DomainEntities.Users; 
using StoreU_WebApi.Controllers;
using StoreU_WebApi.Model;

using Microsoft.AspNetCore.Mvc;
 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using Xunit;
using StoreU_UnitTests.User;
using StoreU_WebApi.Services.Users;
using StoreU_WebApi.Helpers;

namespace StoreU_UnitTests
{
    public class UserUnitTest
    {
        private UserController _userController;

        public UserUnitTest()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users, UserDto>().ReverseMap();
            });

            var mapper = mapperConfig.CreateMapper();

            var context = new StoreUContext();

            var repository = new UsersService(context);

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();
            var logger = new Logger<UserController>(new NullLoggerFactory());

            _userController = new UserController(mapper, configuration, logger, repository);
        }

        //[Fact]
        //public void GetUser_Test()
        //{
        //    var response = _userController.GetUser(); 
        //    var result = (OkObjectResult)response.Result; 
        //    var value = (List<UserDto>)result.Value;

        //    Assert.True(value.Count > 0);
        //}

        [Fact]
        public void GeneratePassword()
        {
        }

        [Fact]
        public void AddUsers_Test()
        {
            try
            {


                var password = PasswordHelper.CreatePasswordHash("Eduardo0$");
                 
                var response = _userController.Register(new UserDto
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "Eduardo",
                    LastName = "Alvarado",
                    UserName = "eduardo96_@live.com",
                    RoleId = 90,
                    PasswordRaw = "Eduardo0$",
                    Password= password.PasswordSalt,
                    RegistryDate = DateTime.Now,
                   
                }).Result;

            }
            catch (Exception ex)
            {

                //Assert.Throws<Exception>(ex);
            }



        }
    }
}
