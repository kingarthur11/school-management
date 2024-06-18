using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shared.Enums;
using UserService.Models;

namespace Core.Entities
{
    public class User : IdentityUser, ITenant
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string? PhotoUrl { get; set; }
        public PersonaType PesonaType { get; set; }
        public AdminType AdminType { get; set; }
        public bool IsActive { get; set; } = true;
        public string SchoolName { get; set; } = string.Empty;
        // public DateTime CreatedAt { get; set; }
        public Guid? SubscriptPlanId { get; set; }
        public string? TenantId { get; set; }
        public SubscriptPlan? SubscriptPlan { get; set; } = null!;
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;
        public string C_Password { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; protected set; } = string.Empty;
        public virtual DateTime? Deleted { get; protected set; }
        public string? CreatedBy { get; protected set; } = string.Empty;
        public virtual DateTime Created { get; protected set; }
        public virtual DateTime? Modified { get; protected set; }
        public virtual string? LastModifiedBy { get; protected set; }
        public User(DateTime created, bool isDeleted)
        {
            Created = created;
            IsDeleted = isDeleted;
            Id = Guid.NewGuid();
        }
        public User() : this(DateTime.UtcNow, false) { }

        public void Delete(string deletor)
        {
            IsDeleted = true;
            Deleted = DateTime.UtcNow;
            DeletedBy = deletor;
        }

        public void Edit(string editor)
        {
            Modified = DateTime.UtcNow;
            LastModifiedBy = editor;
        }
    }
}