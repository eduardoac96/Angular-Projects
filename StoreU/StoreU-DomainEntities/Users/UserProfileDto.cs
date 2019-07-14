using System;

namespace StoreU_DomainEntities.Users
{
    public class UserProfileDto
    {
        public Guid UserId { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string ProfileTitle { get; set; }
        public string ProfileSummary { get; set; }
        public string Rfc { get; set; }
        public string PhoneNumber { get; set; }
         
    }
}