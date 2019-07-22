using System;
using System.Collections.Generic;
using System.Text;

namespace StoreU_DomainEntities.Category
{
    public class CategoryProductsDto
    {
        public Guid CompanyId { get; set; }
        public Guid CategoryProductId { get; set; }
        public string CategoryName { get; set; }
    }
}
