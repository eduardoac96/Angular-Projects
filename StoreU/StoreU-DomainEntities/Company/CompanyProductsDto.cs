using System;
using System.Collections.Generic;
using System.Text;

namespace StoreU_DomainEntities.Company
{
    public class CompanyProductsDto
    {
        public Guid CompanyId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Stock { get; set; }
        public DateTime? RegistryDate { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? MinimumShipment { get; set; }
        public Guid? CategoryProductId { get; set; }
    }
}
