using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace Core.Entities
{
    public class Campus : BaseEntity, ITenant
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public List<Grade>? Grades { get; set; }

        public string? TenantId { get; set; }
    }
}
