using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Subscribe
{
    [Route("api/subscribe-plan")]
    [ApiController]
    
    public class SubscriptPlanController : ControllerBase
    {
        private readonly ISubscriptPlanRepo _subscriptPlanRepo;
        public SubscriptPlanController(ISubscriptPlanRepo subscriptPlanRepo)
        {
            _subscriptPlanRepo = subscriptPlanRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptPlan>>> Index()
        {
            return Ok(await _subscriptPlanRepo.GetAllSubscriptPlansAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptPlan>> Show(Guid id)
        {
            var result = await _subscriptPlanRepo.ShowSubscriptPlanAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<SubscriptPlan>> CreateSubscriptPlanAsync([FromBody] SubscriptPlan request)
        {
            if (request == null)
                return BadRequest("");
            var createSub = await _subscriptPlanRepo.AddSubscriptPlan(request);
            return CreatedAtAction(nameof(Show), new { id = request.Id }, createSub);
        }
        // [HttpPut]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<ActionResult<SubscriptPlan>> Edit(int id, UpdatePlanRequest request)
        // {
        //     if (id != request.SubscriptPlan.Id)
        //     {
        //         return BadRequest("SubscriptPlan id mismatch");
        //     }
        //     var subToUpdate = await _subscriptPlanRepo.ShowSubscriptPlanAsync(id);
        //     if (subToUpdate == null)
        //         return NotFound("SubscriptPlan id not found");
        //     return await _subscriptPlanRepo.UpdateSubscriptPlan(subToUpdate, request.SubscriptBenefitIds);
        // }
        // [HttpDelete]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<ActionResult> Delete(int id)
        // {
        //     var subToUpdate = await _subscriptPlanRepo.ShowSubscriptPlanAsync(id);
        //     if (subToUpdate == null)
        //     {
        //         return NotFound("SubscriptPlan id not found");
        //     }
        //     await _subscriptPlanRepo.DeleteSubscriptPlan(id);
        //     return Ok($"SubscriptPlan with id {id} deleted successfully");
        // }
    }
}