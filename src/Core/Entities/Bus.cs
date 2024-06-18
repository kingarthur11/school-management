using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace Core.Entities
{
    public class Bus : BaseEntity, ITenant
    {
        [StringLength(50)]
        public string Number { get; set; } = string.Empty;
        public int NumberOfSeat { get; set; }
        public string? TenantId { get; set; }
    }
}
