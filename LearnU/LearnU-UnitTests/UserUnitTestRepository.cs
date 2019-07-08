using LearnU_WebApi.Models;
using LearnU_WebApi.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnU_UnitTests
{
    public class UserUnitTestRepository : IUserRepository
    {
        private readonly List<Users> _users;

        public UserUnitTestRepository()
        {
            _users = new List<Users>();
            _users.Add(new Users
            {
                UserId = Guid.NewGuid(),
                Name = "User1",
                LastName = "LastName1",
                UserName = "user1",
                RoleId = 10,
                PasswordHash = new byte[10],
                Password = new byte[10],
                RegistryDate = DateTime.Now
            });
            _users.Add(new Users
            {
                UserId = Guid.NewGuid(),
                Name = "User2",
                LastName = "LastName2",
                UserName = "user2",
                RoleId = 20,
                PasswordHash = new byte[10],
                Password = new byte[10],
                RegistryDate = DateTime.Now
            });
        }

        public void AddUser(Users postToAdd, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Users>> GetUsers()
        {
            return Task.FromResult(_users.AsEnumerable());
        }

        public Task<(int TotalUsers, IEnumerable<Users> PaginatedUsers)> GetUsersPaginated(string sortField, string sortDirection, int maxRecordsPerPage, int pageIndex, string id, string displayName, string username, string role)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Users> VerifyCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
