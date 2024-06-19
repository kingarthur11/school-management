using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Shared.Models.Requests;
using Shared.Models.Responses;

namespace Core.Interfaces.Repositories
{
    public interface ISubscriptPlanRepo
    {
        Task<ApiResponse<List<SubscribePlanResponse>>> GetAllSubscriptPlansAsync();
        // Task<IEnumerable<SubscriptPlan>> GetAllSubscriptPlansAsync();
        Task<ApiResponse<SubscribePlanResponse>> ShowSubscriptPlanAsync(Guid id);
        Task<BaseResponse> AddSubscriptPlan(CreateSubPlan request);
        // Task<SubscriptPlan> UpdateSubscriptPlan(SubscriptPlan request, List<int> subscriptBenefitIds);
        // Task DeleteSubscriptPlan(string id);
    }
}