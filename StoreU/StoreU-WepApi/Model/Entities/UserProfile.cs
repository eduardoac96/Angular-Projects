using System;
using System.Collections.Generic;

namespace StoreU_WepApi.Model
{
    public partial class UserProfile
    {
        public Guid UserId { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string ProfileTitle { get; set; }
        public string ProfileSummary { get; set; }
        public string Rfc { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Users Users { get; set; }
    }
}
