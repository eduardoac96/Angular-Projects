using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreU_WebApi.Model
{
    public partial class CategoryProducts
    {

        //public Guid UserId { get; set; }
        //public Guid BankId { get; set; }
        //public string CardNumber { get; set; }

        //public virtual Banks Bank { get; set; }
        //public virtual Users User { get; set; }

        [Key] 
        public Guid CategoryProductId { get; set; }
        public Guid CompanyId { get; set; }
        public string CategoryName { get; set; }

        public virtual Company Company { get; set; }
         
    }
}
