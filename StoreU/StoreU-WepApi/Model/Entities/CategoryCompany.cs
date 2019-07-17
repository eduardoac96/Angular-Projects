using System;
using System.Collections.Generic;

namespace StoreU_WebApi.Model
{
    public partial class CategoryCompany
    {
        public CategoryCompany()
        {
            Company = new HashSet<Company>();
        }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Company> Company { get; set; }
    }
}
