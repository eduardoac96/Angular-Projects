using System;
using System.Collections.Generic;

namespace LearnU_WebApi.Models
{
    public partial class UserMedia
    {
        public Guid UserId { get; set; }
        public Guid SocialMediaId { get; set; }

        public virtual SocialMedia SocialMedia { get; set; }
        public virtual Users User { get; set; }
    }
}
