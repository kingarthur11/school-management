using System.Net.Mime;
using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Models.Requests;
using Shared.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserService.Models.ResponseBody;
using System.Security.Claims;

namespace API.Controllers.Auth
{
    [Route("api/user")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthRepository _authRepository;
                // private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(
            // IHttpContextAccessor httpContextAccessor,
            IAuthRepository usersRepository)
        {
            // _httpContextAccessor = httpContextAccessor;
            _authRepository = usersRepository;
        }
        [HttpGet("email")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetEmailClaim()
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                var email = emailClaim.Value;
                return Ok(new { Email = email });
            }
            else
            {
                return NotFound("Email claim not found for the user");
            }
        }
        // [HttpGet("jwt")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<ActionResult> Get()
        // {
        //     var emailClaim = User.FindFirst(ClaimTypes.Email);
        //     // string name = await _authRepository.GetJwtResponse();
        //     return Ok(emailClaim);
        // }
        // [SwaggerOperation(
        //       Summary = "Create a new Parent Endpoint",
        //       Description = "This endpoint creates a new Parent. It requires Admin privilege",
        //       OperationId = "parent.create",
        //       Tags = new[] { "PersonaEndpoints" })
        // ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<UserResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ApiResponse<List<UserResponse>>>> Index()
        {
            var response = await _authRepository.GetAllUsersAsync();
            return HandleResult(response);
            // return await _authRepository.GetAllUsersAsync();
        }
        // [SwaggerOperation(
        //       Summary = "Create a new Parent Endpoint",
        //       Description = "This endpoint creates a new Parent. It requires Admin privilege",
        //       OperationId = "parent.create",
        //       Tags = new[] { "PersonaEndpoints" })
        // ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("user_id/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ApiResponse<UserResponse>>> Show(Guid id)
        {
            // var jwtIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            // if (jwtIdClaim != null)
            // {
            //     var jwtId = jwtIdClaim.Value;
            //     return Ok(new { JwtId = jwtId });
            // }
            // else
            // {
            //     // Handle case where "jti" claim is not found
            //     return NotFound();
            // }
            var response = await _authRepository.ShowUserByIdAsync(id);
            return HandleResult(response);
            
        }
        // [SwaggerOperation(
        //       Summary = "Create a new Parent Endpoint",
        //       Description = "This endpoint creates a new Parent. It requires Admin privilege",
        //       OperationId = "parent.create",
        //       Tags = new[] { "PersonaEndpoints" })
        // ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("user_email/{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ApiResponse<UserResponse>>> ShowByEmail(string email)
        {
            var response = await _authRepository.ShowUserByEmailAsync(email);
            return HandleResult(response);
            // var user = await _authRepository.ShowUserByEmailAsync(email);
            // if (user == null)
            // {
            //     return NotFound();
            // }
            // return Ok(user);
        }
        // [SwaggerOperation(
        //       Summary = "Create a new Parent Endpoint",
        //       Description = "This endpoint creates a new Parent. It requires Admin privilege",
        //       OperationId = "parent.create",
        //       Tags = new[] { "PersonaEndpoints" })
        // ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<AuthenticateResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("/register")]
        public async Task<ActionResult<ApiResponse<AuthenticateResponse>>> Register(CreateUserDTO request)
        {
            try
            {
                var response = await _authRepository.RegisterUserAsync(request);
                return HandleResult(response);
                // return Ok(response);
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
        }
        // [SwaggerOperation(
        //       Summary = "Create a new Parent Endpoint",
        //       Description = "This endpoint creates a new Parent. It requires Admin privilege",
        //       OperationId = "parent.create",
        //       Tags = new[] { "PersonaEndpoints" })
        // ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<AuthenticateResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("/login")] 
        public async Task<ActionResult<ApiResponse<AuthenticateResponse>>> Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = await _authRepository.Login(model);
                return HandleResult(response);
                // var response = await _authRepository.Login(model);
                // return Ok(response);
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
        }
        

        // private string GenerateJwtToken(IdentityUser user)
        // {
        //     if (user == null)
        //     {
        //         throw new ArgumentNullException(nameof(user), "User cannot be null");
        //     }
        //     var jwtTokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        //     var tokenDescriptor = new SecurityTokenDescriptor()
        //     {
        //         Subject = new ClaimsIdentity(new [] 
        //         {
        //             new Claim("id", user.Id),
        //             new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //             new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //             new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
        //             new Claim(JwtRegisteredClaimNames.Sub, DateTime.Now.ToUniversalTime().ToString()),
        //         }),
        //         Expires = DateTime.Now.AddDays(1),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        //     };
        //     var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        //     var jwtToken = jwtTokenHandler.WriteToken(token);
        //     return jwtToken;
        // }
        // [HttpPost("login")] 
        // public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        // {
        //     var response = await _authRepository.Authenticate(model);
        //     if (response == null)
        //         return BadRequest(new { message = "Username or password is incorrect" });
        //     return Ok(response);
        // }
        // [HttpGet("test-user")]
        // // [Authorize]
        // public async Task<ActionResult<User>> TestUser(AuthenticateRequest model)
        // {
        //     var response = await _authRepository.TestUserAsyn(model);
        //     return Ok(response);
        // }
        // [HttpPost("login")] 
        // public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        // {
        //     var response = await _authRepository.Authenticate(model);
        //     if (response == null)
        //         return BadRequest(new { message = "Username or password is incorrect" });
        //     return Ok(response);
        // }
        // [HttpGet("test-user")]
        // [Authorize]
        // public async Task TestUser()
        // {
        //     Console.WriteLine("hellow world");
        //     Console.WriteLine("User Id: " +
        //     Console.WriteLine("Username: " + _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Authentication)); 
        // }
    }
}