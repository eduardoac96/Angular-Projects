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
using StoreU_WepApi.Helpers;
using StoreU_WepApi.Model.Entities;

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

            int activationCode = calculateActivationCode();

            string tokenUser = EncryptionHelper.Encrypt(user.UserId.ToString(), "pkcs12-DEF");

            _context.UserVerificationCode.Add(new UserVerificationCode
            {
                VerificationId = new Guid(),
                UserId = user.UserId,
                RegistryDate = DateTime.Now,
                ExpirationTime = DateTime.Now.AddHours(2),
                SecurityCode = activationCode, 
            });

            await _context.SaveChangesAsync();

            sendEmailGenerationCode(email,tokenUser);

            return new UserResponseEmailDto
            {
                UserId = user.UserId,
                Email = user.UserName,
                VerificationCode = activationCode,
                TokenUser = tokenUser
            };
        }

        //Tuple<string, string> GenerateToken(Guid userId, int role, string appSecret);

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


        private int calculateActivationCode()
        {
            Random r = new Random();
            int randNum = r.Next(1000000);

            return randNum;
        }

        private void sendEmailGenerationCode(string email, string tokenUser)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("eduardo96_@live.com");
            message.To.Add(new MailAddress(email));
            message.Subject = "Store U - Resetear Contraseña";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = generateTemplate(email, tokenUser);
            smtp.Port = 587;
            smtp.Host = "smtp.live.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("eduardo96_@live.com","28cadames2013");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.SendAsync(message,null);
        }


        public async Task ValidateCode(string email, int codeNumber)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email no puede estar vacio");

            if(codeNumber == 0)
                throw new Exception("Código no puede estar vacio");

            if (codeNumber.ToString().Length < 6)
                throw new Exception("Código debe de ser de 6 caracteres");


            var emailAddressAttribute = new EmailAddressAttribute();

            if (!emailAddressAttribute.IsValid(email))
                throw new Exception("Formato de email invalido");

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == email);

            if (user == null)
                throw new Exception("Email no existe");
             
            var userCode = await _context.UserVerificationCode.OrderByDescending(p=> p.RegistryDate)
                                .FirstOrDefaultAsync(x => x.UserId == user.UserId);


            if (userCode == null)
                throw new Exception("No existe código de validación");


            if (DateTime.Now > userCode.ExpirationTime)
                throw new Exception("Código de validación ya expirado");

            if(userCode.SecurityCode != codeNumber)
                throw new Exception("Código de validación incorrecto.");

            if (userCode.IsUsed)
                throw new Exception("Código de validación ya utilizado previamente");


            userCode.IsUsed = true;
            _context.Update(userCode);
            await SaveAsync();

        }
         
        public async Task SetPassword(UserChangePasswordDto userChangePassword)
        {
            if (userChangePassword.PasswordRaw != userChangePassword.PasswordConfirmation)
                throw new Exception("Contraseña y confirmación deben de coincidir");

            var user = _context.Users.SingleOrDefault(x => x.UserId == userChangePassword.UserId);

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

            //var htmlStream = assembly.GetManifestResourceStream("~/Assets/EmailTemplates/ForgotPassword/GenerationCode.html");

            var htmlStream = Path.GetFullPath("~/Assets/EmailTemplates/ForgotPassword/GenerationCode.html").Replace("~\\", "");


            // Perform replacements on the HTML file (if you're using it as a template).
            var reader = new StreamReader(htmlStream);
             
            var body = reader
                .ReadToEnd()
                .Replace("{0}", name)
                .Replace("{1}", "http://localhost:4200/forgot/reset/" + tokenUser); // and so on...

            return body;
        }

        
    }
}
