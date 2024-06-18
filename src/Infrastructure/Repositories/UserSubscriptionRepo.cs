using System.Security.Claims;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Responses;
using UserService.Models.RequestBody;
using static Shared.Constants.StringConstants;

namespace Core.Interfaces.Repositories
{
    public class UserSubscriptionRepo : IUserSubscriptionRepo
    {
        private readonly AppDbContext _dbContext;
        // private readonly IHttpContextAccessor _httpContextAccessor;
        // private readonly UserManager<User> _userManager;
        public UserSubscriptionRepo(
            // IHttpContextAccessor httpContextAccessor, 
            // UserManager<User> userManager,
            AppDbContext dataContex)
        {
            _dbContext = dataContex;
            // _httpContextAccessor = httpContextAccessor;
            // _userManager = userManager;
        }
        // public async Task<UserSubscript> ShowUserSubscriptAsync()
        // {
        //     var userEmail =  _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier); 
        //     var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
        //     return await _dbContext.UserSubscripts.FirstOrDefaultAsync(subscription => subscription.UserId == user.Id);
        // }

        public async Task<BaseResponse> AddAsync(Trip trip)
        {
            var response = new BaseResponse() { Code = ResponseCodes.Status201Created };

            _dbContext.Trips.Add(trip);

            var result = await _dbContext.TrySaveChangesAsync();
            if (result)
            {
                return response;
            }

            response.Message = "Unable to create new trip! Please try again";
            response.Status = false;
            response.Code = ResponseCodes.Status500InternalServerError;

            return response;
        }
        public async Task<IEnumerable<UserSubscript>> GetAllUserSubscriptsAsync()
        {
            return await _dbContext.UserSubscripts.ToListAsync();
        }
        // // public async Task<UserSubscript> AddUserSubscript(SubscribeRequest request)
        public async Task<BaseResponse> AddUserSubscript(SubscribeRequest request)
        {
            var subPlan = await _dbContext.SubscriptPlans.FirstOrDefaultAsync(sub => sub.Id == request.SubscriptPlanId);
            var response = new BaseResponse() { Code = ResponseCodes.Status201Created };

            UserSubscript plan = new();
            if (plan == null)
            {
                plan.SubscriptPlanId = request.SubscriptPlanId;
                plan.PersonaId = Guid.NewGuid();
                plan.SubscriptPlan = subPlan;
            }
            await _dbContext.UserSubscripts.AddAsync(plan);
            var result = await _dbContext.TrySaveChangesAsync();
            if (result)
            {
                return response;
            }

            response.Message = "Unable to add student to the trip! Please try again";
            response.Status = false;
            response.Code = ResponseCodes.Status500InternalServerError;

            return response;
        }
        
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