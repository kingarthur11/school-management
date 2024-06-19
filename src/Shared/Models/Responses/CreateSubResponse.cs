using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Models.Responses
{
    public class CreateSubResponse
    {
        public Guid SubscriptPlanId { get; set;}
        public string PersonaId { get; set; }
    }
}