using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SubscriptPlanRepo : ISubscriptPlanRepo
    {
        private readonly AppDbContext _dbContext;
        public SubscriptPlanRepo(AppDbContext dataContex)
        {
            _dbContext = dataContex;
        }
        public async Task<SubscriptPlan> ShowSubscriptPlanAsync(Guid id)
        {
            return await _dbContext.SubscriptPlans.FirstOrDefaultAsync(subscription => subscription.Id == id);
        }
        public async Task<IEnumerable<SubscriptPlan>> GetAllSubscriptPlansAsync()
        {
            return await _dbContext.SubscriptPlans.ToListAsync();
        }
        public async Task<SubscriptPlan> AddSubscriptPlan(SubscriptPlan request)
        {
            var result = await _dbContext.SubscriptPlans.AddAsync(request);
            await _dbContext.SaveChangesAsync();
            SubscriptPlanBenefit planbenefit = new();
            if (request.SubscriptPlanBenefits == null)
            {
                var subscriptBenefits = request.SubscriptPlanBenefits;
                foreach (var subscriptBenefit in subscriptBenefits)
                {
                    planbenefit.SubscriptBenefitsId = subscriptBenefit.Id;
                    planbenefit.SubscriptPlanId = request.Id;
                    planbenefit.SubscriptPlan = request;
                    planbenefit.SubscriptBenefits = _dbContext.SubscriptBenefits.Where(plan => plan.Id == subscriptBenefit.Id).FirstOrDefault();
                    _dbContext.SubscriptPlanBenefits.AddAsync(planbenefit);
                    _dbContext.SaveChangesAsync();
                }
            }
            return result.Entity;
        }
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