using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces.Repositories
{
    public class SubscriptBenefitRepo : ISubscriptBenefitRepo
    {
        private readonly AppDbContext _dbContext;
        public SubscriptBenefitRepo(AppDbContext dataContex)
        {
            _dbContext = dataContex;
        }
        public async Task<SubscriptBenefits> GetSubscriptBenefitAsync(Guid id)
        {
            return await _dbContext.SubscriptBenefits.FirstOrDefaultAsync(subscription => subscription.Id == id);
        }
        public async Task<IEnumerable<SubscriptBenefits>> GetSubscriptBenefitListAsync()
        {
            return await _dbContext.SubscriptBenefits.ToListAsync();
        }
        // public async Task<SubscriptBenefits> AddSubscriptBenefitAsync(CreateBenefitRequest request)
        // {
        //     var result = await _dataContext.SubscriptBenefits.AddAsync(request.SubscriptBenefits);
        //     _dataContext.SaveChangesAsync();
        //     SubscriptPlanBenefit planbenefit = new();
        //     if (request.SubscriptPlanId == null)
        //     {
        //         var subscriptPlanIds = request.SubscriptPlanId;
        //         foreach (var subscriptPlanId in subscriptPlanIds)
        //         {
        //             planbenefit.SubscriptPlanId = subscriptPlanId;
        //             planbenefit.SubscriptBenefitsId = request.SubscriptBenefits.Id;
        //             planbenefit.SubscriptBenefits = request.SubscriptBenefits;
        //             planbenefit.SubscriptPlan = _dataContext.SubscriptPlans.Where(plan => plan.Id == subscriptPlanId).FirstOrDefault();
        //             _dataContext.SubscriptPlanBenefits.AddAsync(planbenefit);
        //             _dataContext.SaveChangesAsync();
        //         }
        //     }

        //     return result.Entity;
        // }
        // public async Task<SubscriptBenefits> UpdateSubscriptBenefitAsync(SubscriptBenefits request, List<int> subscriptPlanIds)
        // {
        //     var result = await _dataContext.SubscriptBenefits.FirstOrDefaultAsync(sub => sub.Id == request.Id);
        //     if (result == null)
        //     {
        //         return null;
        //     }
        //     result.Name = request.Name;
        //     SubscriptPlanBenefit planbenefit = new();
        //     if (subscriptPlanIds == null)
        //     {
        //         foreach (var subscriptPlanId in subscriptPlanIds)
        //         {
        //             planbenefit.SubscriptPlanId = subscriptPlanId;
        //             planbenefit.SubscriptBenefitsId = request.Id;
        //             planbenefit.SubscriptBenefits = request;
        //             planbenefit.SubscriptPlan = _dataContext.SubscriptPlans.Where(plan => plan.Id == subscriptPlanId).FirstOrDefault();
        //             _dataContext.SubscriptPlanBenefits.AddAsync(planbenefit);
        //             _dataContext.SaveChangesAsync();
        //         }
        //     }
        //     await _dataContext.SaveChangesAsync();
        //     return result;
        // }
        // public async Task DeleteSubscriptBenefitAsync(string id)
        // {
        //     var result = await _dataContext.SubscriptBenefits.FirstOrDefaultAsync(sub => sub.Id == id);
        //     if (result != null)
        //     {
        //         _dataContext.SubscriptBenefits.Remove(result);
        //         await _dataContext.SaveChangesAsync();
        //     }
        // }
    }
}