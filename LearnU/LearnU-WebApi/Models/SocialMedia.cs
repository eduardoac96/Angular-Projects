using System;
using System.Collections.Generic;

namespace LearnU_WebApi.Models
{
    public partial class SocialMedia
    {
        public SocialMedia()
        {
            UserMedia = new HashSet<UserMedia>();
        }

        public Guid SocialMediaId { get; set; }
        public string SocialMediaName { get; set; }

        public virtual ICollection<UserMedia> UserMedia { get; set; }
    }
}
