using System;
using System.Collections.Generic;

namespace StoreU_WebApi.Model
{
    public partial class UserBank
    {
        public Guid UserId { get; set; }
        public Guid BankId { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }

        public virtual BankId Bank { get; set; }
        public virtual Users User { get; set; }
    }
}
