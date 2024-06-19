using System.Net.Mime;
using System.Security.Claims;
using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Models.Responses;
using UserService.Models.RequestBody;
using UserService.Models.ResponseBody;

namespace API.Controllers.Subscribe
{
    [Route("api/subscribe")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserSubscriptController : BaseController
    {
        private readonly IUserSubscriptionRepo _userSubscriptionRepo;
        public UserSubscriptController(IUserSubscriptionRepo userSubscriptionRepo)
        {
            _userSubscriptionRepo = userSubscriptionRepo; 
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _userSubscriptionRepo.GetAllUserSubscriptsAsync());
        }
        // [HttpGet("user")]
        // public async Task<ActionResult<UserSubscript>> Show()
        // {
        //     var result = await _userSubscriptionRepo.ShowUserSubscriptAsync();
        //     if (result == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(result);
        // }
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<CreateSubResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("user")]
        public async Task<ActionResult<BaseResponse>> Create([FromBody] SubscribeRequest request)
        {
            try
            {
                var tenantIdClaim = HttpContext.User.FindFirst("TenantId");
                var emailClaim = User.FindFirst(ClaimTypes.Email);
                var tenantId = tenantIdClaim.Value;
                var email = emailClaim.Value;

                var response = await _userSubscriptionRepo.AddUserSubscript(request, tenantId, email);
                // return Ok(response);
                return HandleResult(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error" + ex.Message });
            }
            // if (request == null)
            //     Console.WriteLine("error creating user plan");
            // var createSub = await _userSubscriptionRepo.AddUserSubscript(request);
            // return createSub;
        }
    }
}