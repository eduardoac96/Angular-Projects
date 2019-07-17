using System;
using System.Collections.Generic;

namespace StoreU_WebApi.Model
{
    public partial class BankId
    {
        public BankId()
        {
            UserBank = new HashSet<UserBank>();
        }

        public Guid BankId1 { get; set; }
        public string BankName { get; set; }

        public virtual ICollection<UserBank> UserBank { get; set; }
    }
}
