using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Subscribe
{
    [Route("api/subscribe")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserSubscriptController : ControllerBase
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
        // [HttpPost("user")]
        // public async Task<IActionResult> Create([FromBody] SubscribeRequest request)
        // {
        //     try
        //     {
        //          var response = await _userSubscriptionRepo.AddUserSubscript(request);
        //         // var response = await _authRepository.RegisterUserAsync(request);
        //         return Ok(response);
        //     }
        //     catch (NotFoundException ex)
        //     {
        //         return NotFound(new { error = ex.Message });
        //     }
        //     catch (BadRequestException ex)
        //     {
        //         return BadRequest(new { error = ex.Message });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { error = "Internal Server Error" + ex.Message });
        //     }
        //     // if (request == null)
        //     //     Console.WriteLine("error creating user plan");
        //     // var createSub = await _userSubscriptionRepo.AddUserSubscript(request);
        //     // return createSub;
        // }
    }
}