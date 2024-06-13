using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Shared.Models.Responses;

namespace Core.Interfaces.Repositories
{
    public interface ISubscriptPlanRepo
    {
        Task<IEnumerable<SubscriptPlan>> GetAllSubscriptPlansAsync();
        Task<SubscriptPlan> ShowSubscriptPlanAsync(Guid id);
        Task<BaseResponse> AddSubscriptPlan(SubscriptPlan request);
        // Task<SubscriptPlan> UpdateSubscriptPlan(SubscriptPlan request, List<int> subscriptBenefitIds);
        // Task DeleteSubscriptPlan(string id);
    }
}