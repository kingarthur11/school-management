using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace Core.Entities.Users
{
    public class Staff : BaseEntity, ITenant
    {
        //UserId or PersonaId
        public string? PersonaId { get; set; }

        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string? PhotoUrl { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Guid? JobTitleId { get; set; }
        public JobTitle? JobTitle { get; set; }
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? TenantId { get; set; }
        public void Delete(string deletor)
        {
            IsDeleted = true;
            Deleted = DateTime.UtcNow;
            DeletedBy = deletor;
        }
    }
}
