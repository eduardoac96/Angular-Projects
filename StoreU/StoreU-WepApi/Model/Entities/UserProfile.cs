﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreU_WebApi.Model
{
    public partial class UserProfile
    {
        [Key]
        public Guid UserId { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string ProfileTitle { get; set; }
        public string ProfileSummary { get; set; }
        public string Rfc { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Users Users { get; set; }
    }
}
