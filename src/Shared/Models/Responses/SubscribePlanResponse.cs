using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Enums;

namespace Shared.Models.Responses
{
    public class SubscribePlanResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string StudentEnrollment { get; set; }
        public AdminType AdminType { get; set; }
    }
}