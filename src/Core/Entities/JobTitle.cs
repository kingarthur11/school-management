using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace Core.Entities
{
    public class JobTitle : BaseEntity, ITenant
    {
  
        public string? TenantId { get; set; }
  
 
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
