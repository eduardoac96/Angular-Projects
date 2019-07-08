using System;
using System.Collections.Generic;

namespace LearnU_WebApi.Models
{
    public partial class UserProfile
    {
        public Guid UserId { get; set; }
        public string ProfilePhoto { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string Extract { get; set; }
        public string Language { get; set; }

        public virtual Users User { get; set; }
    }
}
