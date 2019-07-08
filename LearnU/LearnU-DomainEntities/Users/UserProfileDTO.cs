using System;
using System.Collections.Generic;

namespace LearnU_DomainEntities.Users
{
    public class UserProfileDTO
    {
        public Guid UserId { get; set; }
        public string ProfilePhoto { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string Extract { get; set; }
        public string Language { get; set; }
         
        public List<UserMediaDTO> UserMedia { get; set; }
    }
}