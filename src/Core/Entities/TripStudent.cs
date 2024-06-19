﻿using Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace Core.Entities
{
    public class TripStudent : BaseEntity, ITenant
    {
        public Guid TripId { get; set; }
        public string TenantId { get; set; }
        public Trip Trip { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        // Example of an additional field on the join table
        public DateTime EnrollmentDate { get; set; }
    }
}
