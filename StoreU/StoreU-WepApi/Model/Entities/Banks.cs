using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreU_WebApi.Model
{
    public partial class Banks
    {
        public Banks()
        {
            UserBank = new HashSet<UserBank>();
        }

        [Key]
        public Guid BankId { get; set; }
        public string BankName { get; set; }

        public virtual ICollection<UserBank> UserBank { get; set; }
    }
}
