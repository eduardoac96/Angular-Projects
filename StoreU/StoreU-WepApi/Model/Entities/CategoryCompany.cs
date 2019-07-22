using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreU_WebApi.Model
{
    public partial class CategoryCompany
    { 

        [Key]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual Company Company { get; set; }
         
    }
}
