using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using StoreU_Domain.Users;
using StoreU_DomainEntities.Users;

namespace StoreU_WebApi.Services.Users
{
    public interface IUsersRepository
    {
        Task<bool> SaveAsync();

        Task AddUser(Model.Users userToAdd, string password);

        Task<IEnumerable<Model.Users>> GetUsers();
        Task<(int TotalUsers, IEnumerable<Model.Users> PaginatedUsers)> GetUsersPaginated(string sortField, string sortDirection, int maxRecordsPerPage, int pageIndex, string id, string displayName, string username, string role);

        Task<Model.Users> GetUser(Guid userId);
        Task<Model.Users> VerifyCredentials(string username, string password);

        Task<UserResponseEmailDto> GenerateCode(string email);

        (string Token, string TokenExpiration) GenerateToken(Guid userId, int role, string appSecret);

        Task SetPassword(UserChangePasswordDto userChangePassword);

    }
}
