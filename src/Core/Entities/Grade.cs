using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace Core.Entities
{
    public class Grade : BaseEntity, ITenant
    {
        public string Name { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public int Arms { get; set; }
        public string? TenantId { get; set; }
        public Guid CampusId { get; set; }
        public Campus? Campus { get; set; }
    }
}
