using System;
using System.Collections.Generic;
using System.Text;

namespace StoreU_DomainEntities.Subscription
{
    public class SubscriptionDto
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanDescription { get; set; }
        public decimal? MonthlyCharge { get; set; }

    }
}
