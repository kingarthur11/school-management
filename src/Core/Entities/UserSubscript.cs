using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Shared.Enums;

// using Infrastructure.Identity;

namespace Core.Entities
{
    public class UserSubscript : BaseEntity
    {
        // public int Id { get; set; }
        public Guid SubscriptPlanId { get; set; }
        public SubscriptPlan SubscriptPlan { get; set; } = null!;
        public Guid PersonaId { get; set; }
        public AdminType AdminType { get; set; }
        // public string? TenantId { get; set; }
        // public string UserId { get; set; }
        // public Persona User { get; set; } = null!;
    }
}