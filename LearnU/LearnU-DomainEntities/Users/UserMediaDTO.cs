using System;
using System.ComponentModel.DataAnnotations;

namespace LearnU_DomainEntities.Users
{
    public class UserMediaDTO
    {
        public Guid UserMediaID { get; set; }

        [Required]
        [StringLength(80)]
        public string UserMediaName { get; set; }
    }
}