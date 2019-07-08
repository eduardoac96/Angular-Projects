using System;
using System.Collections.Generic;

namespace LearnU_WebApi.Models
{
    public partial class Users
    {
        public Users()
        {
            UserMedia = new HashSet<UserMedia>();
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? RoleId { get; set; }
        public DateTime? RegistryDate { get; set; }
        public byte[] PasswordHash { get; set; }

        public virtual Roles Role { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<UserMedia> UserMedia { get; set; }
    }
}
