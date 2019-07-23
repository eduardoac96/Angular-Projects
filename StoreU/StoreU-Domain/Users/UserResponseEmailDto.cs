using System;
using System.Collections.Generic;
using System.Text;

namespace StoreU_Domain.Users
{
    public class UserResponseEmailDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public int VerificationCode { get; set; }

        public string TokenUser { get; set; }
    }
}
