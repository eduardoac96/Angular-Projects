using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreU_Domain.Users;
using StoreU_DomainEntities.Users;
using StoreU_WebApi.Helpers;
using StoreU_WebApi.Model;
using StoreU_WebApi.Constants;
using StoreU_WepApi.Helpers;
using StoreU_WepApi.Model.Entities;
using System.Web;

namespace StoreU_WebApi.Services.Users
{
    public class UsersService : IUsersRepository
    {
        private StoreUContext _context;

        public UsersService(StoreUContext context)
        {
            _context = context;
        }

        public async Task AddUser(Model.Users userToAdd, string password)
        {
            userToAdd.UserId = Guid.NewGuid();
            userToAdd.RegistryDate = DateTime.Now;
             
            var passwordResult = password.CreatePasswordHash();

            userToAdd.PasswordHash = passwordResult.PasswordHash;
            userToAdd.Password = passwordResult.PasswordSalt;

            var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.UserName.Trim().ToLower() == userToAdd.UserName.Trim().ToLower());

            if (existingUser != null)
                throw new Exception($"User {userToAdd.UserName} already exists");

            _context.Users.Add(userToAdd);
            await _context.SaveChangesAsync();
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

        public async Task<UserResponseEmailDto> GenerateCode(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email no puede estar vacio");

            var emailAddressAttribute = new EmailAddressAttribute();

            if (!emailAddressAttribute.IsValid(email))
                throw new Exception("Formato de email invalido");

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == email);

            if(user == null)
                throw new Exception("Email no existe");

            //int activationCode = calculateActivationCode();-

            string tokenUser = EncryptionHelper.Encrypt(user.UserId.ToString(), Constants.Constants.TOKEN_KEY);

            tokenUser = HttpUtility.UrlEncode(tokenUser); 

            sendEmailGenerationCode(email,tokenUser);

            return new UserResponseEmailDto
            {
                UserId = user.UserId,
                Email = user.UserName, 
                TokenUser = tokenUser
            };
        }
         
         
            /// <summary>
            /// Returns a Tuple: string Token, string TokenExpiration
            /// </summary>
            /// <param name="userId"></param>
            /// <param name="role"></param>
            /// <param name="appSecret"></param>
            /// <returns></returns>
        public (string Token, string TokenExpiration) GenerateToken(Guid userId, int role, string appSecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(appSecret);

            var expirationDate = DateTime.UtcNow.AddHours(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString()),
                    new Claim(ClaimTypes.Role, role.ToString())
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), new DateTimeOffset(expirationDate, TimeSpan.FromMilliseconds(0)).ToString(Constants.Constants.DATE_TIME_WITH_TIMEZONE_FORMAT));
        }

         
        private void sendEmailGenerationCode(string email, string tokenUser)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(Constants.Constants.EMAILTO);
            message.To.Add(new MailAddress(email));
            message.Subject = "Store U - Resetear Contraseña";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = generateTemplate(email, tokenUser);
            smtp.Port = 587;
            smtp.Host = "smtp.live.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Constants.Constants.EMAILTO, Constants.Constants.EMAILPWD);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.SendAsync(message,null);
        }
          
        public async Task SetPassword(UserChangePasswordDto userChangePassword)
        {
            if (userChangePassword.PasswordRaw != userChangePassword.PasswordConfirmation)
                throw new Exception("Contraseña y confirmación deben de coincidir");
             
            //string tokenUser = HttpUtility.UrlDecode(userChangePassword.UserToken);

            var decryptedToken = EncryptionHelper.Decrypt(userChangePassword.UserToken, Constants.Constants.TOKEN_KEY);
             
            bool isValidUserGuid = Guid.TryParse(decryptedToken, out Guid userGuid);

            if (!isValidUserGuid)
                throw new Exception("Token incorrecto");


            var user = _context.Users.SingleOrDefault(x => x.UserId == userGuid);

            if (user == null)
                throw new Exception("Usuario no existe");

           var passwordResult = PasswordHelper.CreatePasswordHash(userChangePassword.PasswordRaw);
             
            user.PasswordHash = passwordResult.PasswordHash;
            user.Password = passwordResult.PasswordSalt;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

        }

        private string generateTemplate(string name, string tokenUser)
        {
            var assembly = Assembly.GetExecutingAssembly();
             
            var htmlStream = Path.GetFullPath("~/Assets/EmailTemplates/ForgotPassword/GenerationCode.html").Replace("~\\", "");

            // Perform replacements on the HTML file (if you're using it as a template).
            var reader = new StreamReader(htmlStream);
             
            var body = reader
                .ReadToEnd()
                .Replace("{0}", name)
                .Replace("{1}", $"{Constants.Constants.FORGOTRESETURL}{tokenUser}"); // and so on...

            return body;
        }

        
    }
}
