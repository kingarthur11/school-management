using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace Core.Entities
{
    public class Department : BaseEntity, ITenant
    {
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? TenantId { get; set; }
    }
}
