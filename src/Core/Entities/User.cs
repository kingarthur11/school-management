using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        public string SchoolName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Guid? SubscriptPlanId { get; set; }
        public string? TenantId { get; set; }
        public SubscriptPlan? SubscriptPlan { get; set; } = null!;
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;
        public string C_Password { get; set; } = string.Empty;
    }
}