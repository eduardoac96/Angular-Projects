using System;
using System.Collections.Generic;

namespace StoreU_WebApi.Model
{
    public partial class UserAddress
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual Users User { get; set; }
    }
}
