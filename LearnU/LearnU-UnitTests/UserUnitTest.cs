using AutoMapper;
using LearnU_DomainEntities.Users;
using LearnU_WebApi.Controllers;
using LearnU_WebApi.Models;
using LearnU_WebApi.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using Xunit;

namespace LearnU_UnitTests
{
    public class UserUnitTest
    {
        private UserController _controller;
         
        public UserUnitTest()
        {
            var mapperConfig = new MapperConfiguration(cfg => {
                // All necessary mappings
                cfg.CreateMap<Users, UserDisplayDTO>();
            });
            var mapper = mapperConfig.CreateMapper();
            //var repository = new UserServices(new LearnUContext());
            var repository = new UserUnitTestRepository(); 
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();
            var logger = new Logger<UserController>(new NullLoggerFactory());

            _controller = new UserController(mapper, configuration, logger, repository);
        }
        [Fact]
        public void GetUsers_Test()
        {
            var response = _controller.GetUsers();
 
            var result = (OkObjectResult)response.Result;
 
            var value = (List<UserDisplayDTO>)result.Value;
            Assert.True(value.Count > 0);
        }

        [Fact]
        public void AddUsers_Test()
        {
             
                var response = _controller.Register(new UserMaintenanceDTO
                {
                    UserId = Guid.NewGuid(),
                    Name = "User1",
                    LastName = "LastName1",
                    UserName = "user1",
                    RoleID = 10,
                    PasswordHash = new byte[10],
                    Password = new byte[10],
                    RegistryDate = DateTime.Now
                }); 
            

        }
    }
}
