using System;

namespace StoreU_DomainEntities.Users
{
    public class UserAddressDto
    { 
        public Guid AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
         
    }

}