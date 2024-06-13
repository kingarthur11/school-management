using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Subscribe
{
    [Route("api/subscribe-benefits")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubscriptBenefitsController : ControllerBase
    {
        private readonly ISubscriptBenefitRepo _subscriptBenefitRepo;
        public SubscriptBenefitsController(ISubscriptBenefitRepo subscriptBenefitRepo)
        {
            _subscriptBenefitRepo = subscriptBenefitRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptBenefits>>> Index()
        {
            return Ok(await _subscriptBenefitRepo.GetSubscriptBenefitListAsync());

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptBenefits>> Show(Guid id)
        {
            var result = await _subscriptBenefitRepo.GetSubscriptBenefitAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        // [HttpPost]
        // public async Task<ActionResult<SubscriptBenefits>> Create(CreateBenefitRequest request)
        // {
        //     if (request == null)
        //         return BadRequest("");
        //     var createSub = await _subscriptBenefitRepo.AddSubscriptBenefitAsync(request);
        //     return CreatedAtAction(nameof(Show), new { id = request.SubscriptBenefits.Id }, createSub);
        // }
        // [HttpPut]
        // public async Task<ActionResult<SubscriptBenefits>> Edit(string id, UpdateBenefitRequest request)
        // {
        //     if (id != request.SubscriptBenefits.Id)
        //     {
        //         return BadRequest("SubscriptPlan id mismatch");
        //     }
        //     var subToUpdate = await _subscriptBenefitRepo.GetSubscriptBenefitAsync(id);
        //     if (subToUpdate == null)
        //         return NotFound("SubscriptPlan id not found");
        //     return await _subscriptBenefitRepo.UpdateSubscriptBenefitAsync(subToUpdate, request.SubscriptPlanId);
        // }
        // [HttpDelete]
        // public async Task<ActionResult> Delete(int id)
        // {
        //     var subToUpdate = await _subscriptBenefitRepo.GetSubscriptBenefitAsync(id);
        //     if (subToUpdate == null)
        //     {
        //         return NotFound("SubscriptPlan id not found");
        //     }
        //     await _subscriptBenefitRepo.DeleteSubscriptBenefitAsync(id);
        //     return Ok($"SubscriptPlan with id {id} deleted successfully");
        // }
    }
}