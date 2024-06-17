using System.Net.Mime;
using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.Subscribe
{
    [Route("api/subscribe-plan")]
    [ApiController]
    
    public class SubscriptPlanController : BaseController
    {
        private readonly ISubscriptPlanRepo _subscriptPlanRepo;
        public SubscriptPlanController(ISubscriptPlanRepo subscriptPlanRepo)
        {
            _subscriptPlanRepo = subscriptPlanRepo;
        }
        
        [SwaggerOperation(
              Summary = "Create a new Parent Endpoint",
              Description = "This endpoint creates a new Parent. It requires Admin privilege",
              OperationId = "parent.create",
              Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<SubscribePlanResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<SubscribePlanResponse>>>> Index()
        {
            var response = await _subscriptPlanRepo.GetAllSubscriptPlansAsync();
            return HandleResult(response);
        }

        [SwaggerOperation(
              Summary = "Create a new Parent Endpoint",
              Description = "This endpoint creates a new Parent. It requires Admin privilege",
              OperationId = "parent.create",
              Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<SubscribePlanResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<SubscribePlanResponse>>> Show(Guid id)
        {
            var response = await _subscriptPlanRepo.ShowSubscriptPlanAsync(id);
            return HandleResult(response);
        }
        
        // [HttpGet("{id}")]
        // public async Task<ActionResult<SubscriptPlan>> Show(Guid id)
        // {
        //     var result = await _subscriptPlanRepo.ShowSubscriptPlanAsync(id);
        //     if (result == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(result);
        // }
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