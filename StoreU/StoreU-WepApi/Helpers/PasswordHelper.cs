using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreU_WebApi.Helpers
{
    public static class PasswordHelper
    {
        public static (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHash(this string password)
        {
             
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (passwordHash, passwordSalt);
            }
        }

         
        public static bool VerifyPasswordHash(string candidatePassword, byte[] storedHash, byte[] storedSalt)
        {
            if (candidatePassword == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(candidatePassword)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(candidatePassword));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
