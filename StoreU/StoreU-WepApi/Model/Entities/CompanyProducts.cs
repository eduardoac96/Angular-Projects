using System;
using System.Collections.Generic;

namespace StoreU_WebApi.Model
{
    public partial class CompanyProducts
    {
        public Guid CompanyId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Stock { get; set; }
        public DateTime? RegistryDate { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? MinimumShipment { get; set; }
        public Guid? CategoryProductId { get; set; }

        public virtual Company Company { get; set; }
    }
}
