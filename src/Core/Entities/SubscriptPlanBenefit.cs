using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Entities
{
    public class SubscriptPlanBenefit : BaseEntity
    {
        // public int Id { get; set; }
        public Guid SubscriptPlanId { get; set; }
        public SubscriptPlan SubscriptPlan { get; set; } = null!;

        public Guid SubscriptBenefitsId { get; set; }
        public SubscriptBenefits SubscriptBenefits { get; set; } = null!;
    }
}