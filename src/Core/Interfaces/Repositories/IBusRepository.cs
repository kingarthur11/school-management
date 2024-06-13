﻿using Core.Entities;
using Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IBusRepository
    {
        public Task<BaseResponse> AddBus(Bus bus);
        public Task<IQueryable<Bus>> GetAllAsync();
        public Task<Bus> GetBusAsync(Guid id);
    }
}
