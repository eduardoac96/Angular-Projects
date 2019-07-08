using LearnU_DomainEntities.Users;
using LearnU_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnU_WebApi.Services.User
{
    public interface IUserRepository
    {
        bool Save();

        Task<bool> SaveAsync();

        void AddUser(Users userToAdd, string password);


        Task<IEnumerable<Users>> GetUsers();
        Task<(int TotalUsers, IEnumerable<Users> PaginatedUsers)> GetUsersPaginated(string sortField, string sortDirection, int maxRecordsPerPage, int pageIndex, string id, string displayName, string username, string role);

        Task<Users> GetUser(Guid userId);
        Task<Users> VerifyCredentials(string username, string password);

    }
}
