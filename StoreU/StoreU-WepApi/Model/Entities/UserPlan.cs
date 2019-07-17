using System;
using System.Collections.Generic;

namespace StoreU_WebApi.Model
{
    public partial class UserPlan
    {
        public Guid UserId { get; set; }
        public int PlanId { get; set; }
        public DateTime? PayDate { get; set; }
        public bool? Payed { get; set; }

        public virtual SubscriptionPlans Plan { get; set; }
        public virtual Users User { get; set; }
    }
}
