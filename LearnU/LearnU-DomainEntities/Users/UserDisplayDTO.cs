using System;
using System.Collections.Generic;
using System.Text;

namespace LearnU_DomainEntities.Users
{
    public class UserDisplayDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } 
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }

        public string FullName
        {   
            get
            {
                return $"{Name} {LastName} {SecondLastName}";
            }
        }
        public DateTime? BirthDate { get; set; } 
        public DateTime? RegistryDate { get; set; }

        public int RoleID { get; set; }
        public UserProfileDTO UserProfile { get; set; }
        public string Token { get; set; }
        public string TokenExpiration { get; set; }
    }
}
