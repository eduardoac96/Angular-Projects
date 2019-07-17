using System;
using System.Collections.Generic;
using System.Linq;
using StoreU_WebApi.Model;
using System.Threading.Tasks;

namespace StoreU_WebApi.Services.Users
{
    public interface IUsersRepository
    {
        bool Save();

        Task<bool> SaveAsync();
         
        void AddUser(Model.Users userToAdd, string password);


        Task<IEnumerable<Model.Users>> GetUsers();
        Task<(int TotalUsers, IEnumerable<Model.Users> PaginatedUsers)> GetUsersPaginated(string sortField, string sortDirection, int maxRecordsPerPage, int pageIndex, string id, string displayName, string username, string role);

        Task<Model.Users> GetUser(Guid userId);
        Task<Model.Users> VerifyCredentials(string username, string password);

    }
}
