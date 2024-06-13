﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Responses
{
    public record StudentInSchoolResponse
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public bool? BusServiceRequired { get; set; }
        public bool IsInSchool { get; set; }
    }
}
