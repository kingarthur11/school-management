using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Shared.Enums;

namespace Core.Entities
{
    public class SubscriptBenefits : BaseEntity
    {
        // public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubscriptPlanBenefit> SubscriptPlanBenefits { get; set; } = new List<SubscriptPlanBenefit>();
        public AdminType AdminType { get; set; }
    }
}