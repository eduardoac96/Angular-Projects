using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreU_DomainEntities.Users;
using StoreU_WebApi.Helpers;
using StoreU_WebApi.Model;

namespace StoreU_WebApi.Services.Users
{
    public class UsersService : IUsersRepository
    {
        private StoreUContext _context;

        public UsersService(StoreUContext context)
        {
            _context = context;
        }

        public void AddUser(Model.Users userToAdd, string password)
        {
            var passwordResult = password.CreatePasswordHash();

            userToAdd.PasswordHash = passwordResult.PasswordHash;
            userToAdd.Password = passwordResult.PasswordSalt;

            var existingUser = _context.Users.SingleOrDefault(x => x.UserName.Trim().ToLower() == userToAdd.UserName.Trim().ToLower());

            if (existingUser != null)
                throw new Exception($"User {userToAdd.UserName} already exists");

            _context.Users.Add(userToAdd);
            _context.SaveChangesAsync();
        }

        public async Task<Model.Users> GetUser(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<Model.Users>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<(int TotalUsers, IEnumerable<Model.Users> PaginatedUsers)> GetUsersPaginated(string sortField, string sortDirection, int maxRecordsPerPage, int pageIndex, string id, string displayName, string username, string role)
        {
            sortField = sortField.ToLower();
            var isAscending = sortDirection.ToLower() == "asc";
            var skipNumber = pageIndex * maxRecordsPerPage;
            var takeNumber = maxRecordsPerPage;
            var query = _context.Users.AsQueryable();

            // Filtering logic
            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.ToLower();
                query = query.Where(u => u.UserId.ToString().ToLower().Contains(id));
            }
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                displayName = displayName.ToLower();
                query = query.Where(u => u.FirstName.ToLower().Contains(displayName) || u.LastName.ToLower().Contains(displayName));
            }
            if (!string.IsNullOrWhiteSpace(username))
            {
                username = username.ToLower();
                query = query.Where(u => u.UserName.ToLower().Contains(username));
            }

            // Sorting logic
            if (nameof(UserDto.FullName).ToLower() == sortField)
            {
                query = isAscending ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName);
            }
            else if (nameof(UserDto.UserName).ToLower() == sortField)
            {
                query = isAscending ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName);
            }

            var totalUsers = await query.CountAsync();
            var data = await query
                .Skip(skipNumber)
                .Take(takeNumber)
                .ToListAsync();
            return (totalUsers, data);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }

        public async Task<Model.Users> VerifyCredentials(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            // Checking if user exists
            if (user == null)
                return null;

            // Checking given credentials
            if (!PasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.Password))
                return null;

            // Successful Authentication
            return user;
        }
    }
}
