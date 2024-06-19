using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Shared.Models.Responses;
using UserService.Models.RequestBody;

namespace Core.Interfaces.Repositories
{
    public interface IUserSubscriptionRepo
    {
        Task<IEnumerable<UserSubscript>> GetAllUserSubscriptsAsync();
        // Task<UserSubscript> ShowUserSubscriptAsync();
        Task<BaseResponse> AddUserSubscript(SubscribeRequest request, string tenantId, string email);
        // Task<UserSubscript> UpdateUserSubscript(UserSubscript request, List<int> subscriptBenefitIds);
        // Task DeleteUserSubscript(int id);
    }
}