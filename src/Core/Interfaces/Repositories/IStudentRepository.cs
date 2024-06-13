﻿using Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        public IQueryable<Student> GetStudentsWithBusServiceAsync(Guid busId);
        public Task<Student?> GetByIdAsync(Guid id);
    }
}
