using System;
using System.Collections.Generic;
using System.Text;

namespace StoreU_DomainEntities.Users
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHash { get; set; }

        public string PasswordRaw { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName + LastName + SecondLastName}";
            }
        }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegistryDate { get; set; }
        public int? RoleId { get; set; }
         
        public virtual UserProfileDto UserProfile { get; set; }
        public virtual List<UserAddressDto> Address { get; set; }
        public virtual List<UserBankDto> UserBankCollection { get; set; }
        public virtual List<UserCompanyDto> UserCompanyList { get; set; }
        public Guid UserPlanId { get; set; }
        public string Token { get; set; }
        public string TokenExpiration { get; set; }
    }
}
