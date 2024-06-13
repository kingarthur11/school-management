using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IUserSubscriptionRepo
    {
        Task<IEnumerable<UserSubscript>> GetAllUserSubscriptsAsync();
        // Task<UserSubscript> ShowUserSubscriptAsync();
        // Task<UserSubscript> AddUserSubscript(SubscribeRequest request);
        // Task<UserSubscript> UpdateUserSubscript(UserSubscript request, List<int> subscriptBenefitIds);
        // Task DeleteUserSubscript(int id);
    }
}