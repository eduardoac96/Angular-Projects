using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearnU_DomainEntities.Users
{
    public class UserMaintenanceDTO
    {
        public Guid UserId { get; set; }
        [Required]
        [StringLength(80)]
        [EmailAddress]
        public string UserName { get; set; }

        public string PasswordRaw { get; set; }

        public byte[] Password { get; set; }

        public byte[] PasswordHash { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        [Required]
        [StringLength(80)]
        public string LastName { get; set; }
        public string SecondLastName { get; set; } 
        public DateTime? BirthDate { get; set; }
        public DateTime? RegistryDate { get; set; }
         

        public int RoleID { get; set; }
    }
}
