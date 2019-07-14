using System;
using System.Collections.Generic;

namespace StoreU_WepApi.Model
{
    public partial class CategoryProducts
    {
        public Guid CompanyId { get; set; }
        public Guid CategoryProductId { get; set; }
        public string CategoryName { get; set; }

        public virtual Company Company { get; set; }
    }
}
