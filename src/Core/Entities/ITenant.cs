using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public interface ITenant
    {
        public string TenantId { get; set; }
    }
}