using System;
using System.Collections.Generic;

namespace StoreU_WebApi.Model
{
    public partial class UserCompany
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Users User { get; set; }
    }
}
