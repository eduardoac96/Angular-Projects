using System;
using System.Collections.Generic;

namespace StoreU_WebApi.Model
{
    public partial class Company
    {
        public Company()
        {
            CategoryProducts = new HashSet<CategoryProducts>();
            CompanyProducts = new HashSet<CompanyProducts>();
            UserCompany = new HashSet<UserCompany>();
        }

        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public byte[] CompanyLogo { get; set; }
        public string CompanySummary { get; set; }
        public string CompanyWebPage { get; set; }
        public DateTime? RegistryDate { get; set; }
        public DateTime? CompanyBirthDate { get; set; }
        public int? EmployeesQty { get; set; }
        public Guid? CategoryId { get; set; }
        public string MainAddress { get; set; }

        public virtual CategoryCompany Category { get; set; }
        public virtual ICollection<CategoryProducts> CategoryProducts { get; set; }
        public virtual ICollection<CompanyProducts> CompanyProducts { get; set; }
        public virtual ICollection<UserCompany> UserCompany { get; set; }
    }
}
