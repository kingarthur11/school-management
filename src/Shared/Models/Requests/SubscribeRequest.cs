using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models.RequestBody
{
    public class SubscribeRequest
    {
        [Required]
        public Guid SubscriptPlanId { get; set;}
    }
}