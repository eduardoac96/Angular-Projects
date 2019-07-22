using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreU_WebApi.Model
{
    public partial class SubscriptionPlans
    {
        public SubscriptionPlans()
        {
            UserPlan = new HashSet<UserPlan>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanDescription { get; set; }
        public decimal? MonthlyCharge { get; set; }

        public virtual ICollection<UserPlan> UserPlan { get; set; }
    }
}
