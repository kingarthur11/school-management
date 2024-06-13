using System.Security.Claims;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces.Repositories
{
    public class UserSubscriptionRepo : IUserSubscriptionRepo
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        // private readonly UserManager<User> _userManager;
        public UserSubscriptionRepo(
            IHttpContextAccessor httpContextAccessor, 
            // UserManager<User> userManager,
            AppDbContext dataContex)
        {
            _dbContext = dataContex;
            _httpContextAccessor = httpContextAccessor;
            // _userManager = userManager;
        }
        // public async Task<UserSubscript> ShowUserSubscriptAsync()
        // {
        //     var userEmail =  _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier); 
        //     var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
        //     return await _dbContext.UserSubscripts.FirstOrDefaultAsync(subscription => subscription.UserId == user.Id);
        // }
        public async Task<IEnumerable<UserSubscript>> GetAllUserSubscriptsAsync()
        {
            return await _dbContext.UserSubscripts.ToListAsync();
        }
        // // public async Task<UserSubscript> AddUserSubscript(SubscribeRequest request)
        // public async Task<UserSubscript> AddUserSubscript(SubscribeRequest request)
        // {
        //     var subPlan = await _dataContex.SubscriptPlans.FirstOrDefaultAsync(sub => sub.Id == request.SubscriptPlanId);
        //     // if (subPlan == null)
        //     // {
        //     //     return null;
        //     // }
        //     var userEmail =  _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier); 
        //     var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
        //     // return request.SubscriptPlanId;
        //     if (user == null)
        //     {
        //         return null;
        //     }
        //     UserSubscript plan = new();
        //     if (plan == null)
        //     {
        //         plan.SubscriptPlanId = request.SubscriptPlanId;
        //         plan.UserId = user.Id;
        //         plan.SubscriptPlan = subPlan;
        //         plan.User = user;
        //         var result = _dataContex.UserSubscripts.AddAsync(plan);
        //         _dataContex.SaveChangesAsync();
                
        //     }
        //     return plan;
        // }
        
        // public async Task<UserSubscript> AddUserSubscript(SubscribeRequest request)
        // {
        //     var subPlan = await _dbContext.SubscriptPlans.FirstOrDefaultAsync(sub => sub.Id == request.SubscriptPlanId);
        //     if (subPlan == null)
        //     {
        //         throw new NotFoundException($"Subscription plan not found.");
        //     }
            
        //     var userEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier); 
        //     var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
        //     if (user == null)
        //     {
        //         throw new NotFoundException($"User not found.");
        //     }
            
        //     var existingUserSubscript = await _dbContext.UserSubscripts.FirstOrDefaultAsync(us => us.UserId == user.Id && us.SubscriptPlanId == request.SubscriptPlanId);
        //     if (existingUserSubscript != null)
        //     {
        //         throw new BadRequestException($"User already subscribe to this plan.");
        //     }
            
        //     UserSubscript plan = new()
        //     {
        //         SubscriptPlanId = request.SubscriptPlanId,
        //         UserId = user.Id,
        //         SubscriptPlan = subPlan,
        //         User = user
        //     };
            
        //     var result = await _dbContext.UserSubscripts.AddAsync(plan);
        //     await _dbContext.SaveChangesAsync();
            
        //     return result.Entity;
        // }


        // public async Task<UserSubscript> UpdateUserSubscript(UserSubscript request, List<int> subscriptBenefitIds)
        // {
        //     var result = await _dataContex.UserSubscripts.FirstOrDefaultAsync(sub => sub.Id == request.Id);
        //     if (result == null)
        //     {
        //         return null;
        //     }
        //     UserSubscriptBenefit planbenefit = new();
        //     result.Name = request.Name;
        //     result.Price = request.Price;
        //     result.StudentEnrollment = request.StudentEnrollment;
        //     if (subscriptBenefitIds == null)
        //     {
        //         foreach (var subscriptBenefitId in subscriptBenefitIds)
        //         {
        //             planbenefit.SubscriptBenefitsId = subscriptBenefitId;
        //             planbenefit.UserSubscriptId = request.Id;
        //             planbenefit.UserSubscript = request;
        //             planbenefit.SubscriptBenefits = _dataContex.SubscriptBenefits.Where(plan => plan.Id == subscriptBenefitId).FirstOrDefault();
        //             _dataContex.UserSubscriptBenefits.AddAsync(planbenefit);
        //             _dataContex.SaveChangesAsync();
        //         }
        //     }
        //     await _dataContex.SaveChangesAsync();
        //     return result;
        // }
        // // public async Task DeleteUserSubscript(int id)
    }
}