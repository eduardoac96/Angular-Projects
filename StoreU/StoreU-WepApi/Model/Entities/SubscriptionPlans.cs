using System;
using System.Collections.Generic;

namespace StoreU_WebApi.Model
{
    public partial class SubscriptionPlans
    {
        public SubscriptionPlans()
        {
            UserPlan = new HashSet<UserPlan>();
        }

        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanDescription { get; set; }
        public decimal? MonthlyCharge { get; set; }

        public virtual ICollection<UserPlan> UserPlan { get; set; }
    }
}
