using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Entities
{
    public class SubscriptPlan : BaseEntity
    {
        // public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string StudentEnrollment { get; set; }
        public ICollection<SubscriptPlanBenefit> SubscriptPlanBenefits { get; set; } = new List<SubscriptPlanBenefit>();
        public Guid PersonaId { get; set; }
        
        // public ICollection<Persona> Users { get; } = new List<Persona>();

        // public ICollection<User> Users { get; } = new List<User>();
        // public ICollection<UserSubscript> UserSubscripts { get; set; }
    }
}