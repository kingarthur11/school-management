using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface ISubscriptBenefitRepo
    {
        Task<IEnumerable<SubscriptBenefits>> GetSubscriptBenefitListAsync();
        Task<SubscriptBenefits> GetSubscriptBenefitAsync(Guid id);
        // Task<SubscriptBenefits> AddSubscriptBenefitAsync(CreateBenefitRequest request);
        // Task<SubscriptBenefits> UpdateSubscriptBenefitAsync(SubscriptBenefits request, List<int> subscriptPlanIds);
        // Task DeleteSubscriptBenefitAsync(string id);
    }
}