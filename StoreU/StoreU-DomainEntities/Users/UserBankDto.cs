using System;

namespace StoreU_DomainEntities.Users
{
    public class UserBankDto
    {
        public Guid UserId { get; set; }
        public Guid BankId { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
         
    }
}