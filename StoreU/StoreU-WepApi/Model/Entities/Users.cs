using System;
using System.Collections.Generic;

namespace StoreU_WepApi.Model
{
    public partial class Users
    {
        public Users()
        {
            UserAddress = new HashSet<UserAddress>();
            UserBank = new HashSet<UserBank>();
            UserCompany = new HashSet<UserCompany>();
            UserPlan = new HashSet<UserPlan>();
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegistryDate { get; set; }
        public int? RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual UserProfile User { get; set; }
        public virtual ICollection<UserAddress> UserAddress { get; set; }
        public virtual ICollection<UserBank> UserBank { get; set; }
        public virtual ICollection<UserCompany> UserCompany { get; set; }
        public virtual ICollection<UserPlan> UserPlan { get; set; }
    }
}
