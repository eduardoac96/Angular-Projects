using System;
using System.Collections.Generic;
using System.Text;

namespace StoreU_DomainEntities.Company
{
    public class CompanyDto
    {
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

    }
}
