using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Responses;
using static Shared.Constants.StringConstants;

namespace Infrastructure.Repositories
{
    public class SubscriptPlanRepo : ISubscriptPlanRepo
    {
        private readonly AppDbContext _dbContext;
        public SubscriptPlanRepo(AppDbContext dataContex)
        {
            _dbContext = dataContex;
        }
        public async Task<ApiResponse<SubscribePlanResponse>> ShowSubscriptPlanAsync(Guid id)
        {
            var result = await _dbContext.SubscriptPlans.FirstOrDefaultAsync(subscription => subscription.Id == id);
            if (result == null)
            {
                return new ApiResponse<SubscribePlanResponse>()
                {
                    Data = null,
                    Message = "Subscription plan not found"
                };
            }
            return new ApiResponse<SubscribePlanResponse>()
            {
                Data = new SubscribePlanResponse()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Price = result.Price,
                    StudentEnrollment = result.StudentEnrollment
                }
            };
        }
        public async Task<ApiResponse<List<SubscribePlanResponse>>> GetAllSubscriptPlansAsync()
        {
            var response = new ApiResponse<List<SubscribePlanResponse>>();
            var students = _dbContext.SubscriptPlans
                .Select(x => new SubscribePlanResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    StudentEnrollment = x.StudentEnrollment
                })
                .AsNoTracking();

            response.Data = await students.ToListAsync();
            return response;
        }

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
        public async Task<BaseResponse> AddSubscriptPlan(SubscriptPlan request)
        {
            var response = new BaseResponse() { Code = ResponseCodes.Status201Created };

            await _dbContext.SubscriptPlans.AddAsync(request);

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
        // public async Task<SubscriptPlan> AddSubscriptPlan(SubscriptPlan request)
        // {
        //     var result = await _dbContext.SubscriptPlans.AddAsync(request);
        //     await _dbContext.SaveChangesAsync();
        //     SubscriptPlanBenefit planbenefit = new();
        //     if (request.SubscriptPlanBenefits == null)
        //     {
        //         var subscriptBenefits = request.SubscriptPlanBenefits;
        //         foreach (var subscriptBenefit in subscriptBenefits)
        //         {
        //             planbenefit.SubscriptBenefitsId = subscriptBenefit.Id;
        //             planbenefit.SubscriptPlanId = request.Id;
        //             planbenefit.SubscriptPlan = request;
        //             planbenefit.SubscriptBenefits = _dbContext.SubscriptBenefits.Where(plan => plan.Id == subscriptBenefit.Id).FirstOrDefault();
        //             _dbContext.SubscriptPlanBenefits.AddAsync(planbenefit);
        //             _dbContext.SaveChangesAsync();
        //         }
        //     }
        //     return result.Entity;
        // }
        // public async Task<SubscriptPlan> UpdateSubscriptPlan(SubscriptPlan request, List<int> subscriptBenefitIds)
        // {
        //     var result = await _dataContex.SubscriptPlans.FirstOrDefaultAsync(sub => sub.Id == request.Id);
        //     if (result == null)
        //     {
        //         return null;
        //     }
        //     SubscriptPlanBenefit planbenefit = new();
        //     result.Name = request.Name;
        //     result.Price = request.Price;
        //     result.StudentEnrollment = request.StudentEnrollment;
        //     if (subscriptBenefitIds == null)
        //     {
        //         foreach (var subscriptBenefitId in subscriptBenefitIds)
        //         {
        //             planbenefit.SubscriptBenefitsId = subscriptBenefitId;
        //             planbenefit.SubscriptPlanId = request.Id;
        //             planbenefit.SubscriptPlan = request;
        //             planbenefit.SubscriptBenefits = _dataContex.SubscriptBenefits.Where(plan => plan.Id == subscriptBenefitId).FirstOrDefault();
        //             _dataContex.SubscriptPlanBenefits.AddAsync(planbenefit);
        //             _dataContex.SaveChangesAsync();
        //         }
        //     }
        //     await _dataContex.SaveChangesAsync();
        //     return result;
        // }
        // public async Task DeleteSubscriptPlan(string id)
        // {
        //     var result = await _dataContex.SubscriptPlans.FirstOrDefaultAsync(sub => sub.Id == id);
        //     if (result != null)
        //     {
        //         _dataContex.SubscriptPlans.Remove(result);
        //         await _dataContex.SaveChangesAsync();
        //     }
        // }
    }
}