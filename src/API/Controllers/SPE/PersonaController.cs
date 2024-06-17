﻿using Core.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Requests;
using Models.Responses;
using Shared.Constants;
using Shared.Controllers;
using Shared.Models.Requests;
using Shared.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace API.Controllers.SPE
{
    [ApiExplorerSettings(GroupName = "SPE Module")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonaController : BaseController
    {
        private readonly IPersonaService _personaService;
        private readonly IBusDriverSevice _busDriverSevice;
        private readonly AppDbContext _context;

        public PersonaController(IPersonaService personaService, IBusDriverSevice busDriverSevice, AppDbContext context)
        {
            _personaService = personaService;
            _busDriverSevice = busDriverSevice;
            _context = context;
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
              Summary = "Create a new Parent Endpoint",
              Description = "This endpoint creates a new Parent. It requires Admin privilege",
              OperationId = "parent.create",
              Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ParentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("create-parent")]
        public async Task<ActionResult<ApiResponse<ParentResponse>>> CreateParentAsync([FromForm] CreateParentRequest request)
        {
            var host = Request.Host.ToString();
            var response = await _personaService.CreateParentAsync(request, host);
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
            Summary = "Create a new Student Endpoint",
            Description = "This endpoint creates a new Student. It requires Admin privilege",
            OperationId = "student.create",
            Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<StudentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("create-student")]
        public async Task<ActionResult<ApiResponse<StudentResponse>>> CreateStudentAsync([FromForm] CreateStudentRequest request)
        {
            var host = Request.Host.ToString();
            var response = await _personaService.CreateStudentAsync(request, host);
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Create a new Bus Driver Endpoint",
        Description = "This endpoint creates a new Bus Driver. It requires Admin privilege",
        OperationId = "busDriver.create",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("create-busDriver")]
        public async Task<ActionResult<BaseResponse>> CreateBusDriverAsync([FromForm] CreateBusDriverRequest request)
        {
            var host = Request.Host.ToString();

            var response = await _personaService.CreateBusDriverAsync(request, host);
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Create a new Staff Endpoint",
        Description = "This endpoint creates a new Staff. It requires Admin privilege",
        OperationId = "staff.create",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("create-Staff")]
        public async Task<ActionResult<BaseResponse>> CreateStaffAsync([FromForm] CreateStaffRequest request)
        {
            var host = Request.Host.ToString();

            var response = await _personaService.CreateStaffAsync(request, host);
            return HandleResult(response);
        }

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Get List Of Parents Endpoint",
        Description = "This endpoint gets the list of parents . It requires Admin privilege",
        OperationId = "parents.get",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<ParentResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("parents")]
        public async Task<ActionResult<ApiResponse<List<ParentResponse>>>> GetParentsAsync()
        {
            var response = await _personaService.ParentListAsync();
            return HandleResult(response);
        }

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Get List Of Students For A Parent Endpoint",
        Description = "This endpoint gets the list of students for a Parent. It requires Admin privilege",
        OperationId = "parentstudents.get",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<StudentResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("parent-students")]
        public async Task<ActionResult<ApiResponse<List<StudentResponse>>>> GetParentStudentsAsync(Guid parentId)
        {
            var response = await _personaService.ParentStudentsListAsync(parentId);
            return HandleResult(response);
        }

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Get List Of Students Endpoint",
        Description = "This endpoint gets the list of students . It requires Admin privilege",
        OperationId = "students.get",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<StudentResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("students")]
        public async Task<ActionResult<ApiResponse<List<StudentResponse>>>> GetStudentsAsync()
        {
            var response = await _personaService.StudentListAsync();
            return HandleResult(response);
        }

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Get List Of Staff Endpoint",
        Description = "This endpoint gets the list of staff . It requires Admin privilege",
        OperationId = "staff.get",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<StudentResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("staff")]
        public async Task<ActionResult<ApiResponse<List<StaffResponse>>>> GetStaffAsync()
        {
            var response = await _personaService.StaffListAsync();
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
           Summary = "Get List Of Busdriver Endpoint",
           Description = "This endpoint gets the list of Busdriver. It requires Admin privilege",
           OperationId = "BusDriver.get",
           Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<BusDriverResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("busDriver")]
        public async Task<ActionResult<ApiResponse<List<BusDriverResponse>>>> GetBusDriverAsync()
        {
            var response = await _personaService.BusDriverListAsync();
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Delete A Parent Endpoint",
        Description = "This endpoint deletes a parent. It requires Admin privilege",
        OperationId = "parent.delete",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete("delete-parent")]
        public async Task<ActionResult<BaseResponse>> DeleteParentAsync(Guid parentId)
        {
            var response = await _personaService.DeleteParentAsync(parentId, User.Identity!.Name ?? string.Empty);
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Delete A Student Endpoint",
        Description = "This endpoint deletes a student. It requires Admin privilege",
        OperationId = "student.delete",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete("delete-student")]
        public async Task<ActionResult<BaseResponse>> DeleteStudentAsync(Guid studentId)
        {
            var response = await _personaService.DeleteStudnetAsync(studentId, User.Identity!.Name ?? string.Empty);
            return HandleResult(response);
        }

        //edit student 

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Edit Student Endpoint",
        Description = "This endpoint edits a student. It requires Admin privilege",
        OperationId = "student.edit",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<StudentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut("edit-student")]
        public async Task<ActionResult<ApiResponse<StudentResponse>>> EditStudentAsync(Guid studentId, [FromBody] EditStudentRequest request)
        {
            var response = await _personaService.EditStudentAsync(studentId, request, User.Identity!.Name ?? string.Empty);
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
         Summary = "Edit Parent Endpoint",
         Description = "This endpoint edits a parent. It requires Admin privilege",
         OperationId = "parent.edit",
         Tags = new[] { "PersonaEndpoints" })
         ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut("edit-parent")]
        public async Task<ActionResult<BaseResponse>> EditParentAsync(Guid parentId, [FromBody] EditParentRequest request)
        {
            var response = await _personaService.EditParentAsync(parentId, request, User.Identity!.Name ?? string.Empty);
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Get A Parent Endpoint",
        Description = "This endpoint gets a parent. It requires Admin privilege",
        OperationId = "parent.get",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<ParentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("parentId")]
        public async Task<ActionResult<ApiResponse<ParentResponse>>> GetParentAsync(Guid parentId)
        {
            var response = await _personaService.GetParentAsync(parentId);
            return HandleResult(response);
        }

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Get A Student Endpoint",
        Description = "This endpoint gets a student. It requires Admin privilege",
        OperationId = "student.get",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<StudentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("studentId")]
        public async Task<ActionResult<ApiResponse<StudentResponse>>> GetStudentAsync(Guid studentId)
        {
            var response = await _personaService.GetStudentAsync(studentId);
            return HandleResult(response);
        }

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Get List Of Users Endpoint",
        Description = "This endpoint gets the list of Users . It requires Admin privilege",
        OperationId = "personas.get",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<List<PersonaResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("users")]
        public async Task<ActionResult<ApiResponse<List<PersonaResponse>>>> GetUsersWithRoleAsync()
        {
            var response = await _personaService.GetUsersWithRole();
            return HandleResult(response);
        }

        [AllowAnonymous]
        [SwaggerOperation(
        Summary = "Delete A User Account Endpoint",
        Description = "This endpoint deletes a user account.",
        OperationId = "personaAccount.delete",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete("delete-account")]
        public async Task<ActionResult<BaseResponse>> DeletePersonaAccountAsync(string email)
        {
            var response = await _personaService.DeletePersonaAccountAsync(email);
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Roles.PARENT)]
        [SwaggerOperation(
         Summary = "Update Parent Information Endpoint",
         Description = "This endpoint updates a parent information. It requires Parent privilege",
         OperationId = "parent.update",
         Tags = new[] { "PersonaEndpoints" })
         ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut("update-parent-info")]
        public async Task<ActionResult<BaseResponse>> UpdateParentAsync(Guid parentId, [FromBody] UpdateParentInfoRequest request)
        {
            var response = await _personaService.UpdateParentInfoAsync(parentId, request, User.Identity!.Name ?? string.Empty);
            return HandleResult(response);
        }

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Delete A User By Admin Endpoint",
        Description = "This endpoint deletes a user by admin account. It requires admin privelege",
        OperationId = "persona.deleteByAdmin",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete("delete-user")]
        public async Task<ActionResult<BaseResponse>> DeleteUserByAdminAsync(Guid userId)
        {
            var response = await _personaService.DeleteUserByAdminAsync(userId);
            return HandleResult(response);
        }


        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
         Summary = "Update BusDriver Information Endpoint",
         Description = "This endpoint updates a bus driver information. It requires Admin privilege",
         OperationId = "BusDriver.update",
         Tags = new[] { "PersonaEndpoints" })
         ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut("update-busdriver")]
        public async Task<ActionResult<BaseResponse>> EditBusDriverAsync([FromBody] EditBusdriverRequest request)
        {
            var response = await _busDriverSevice.EditBusdriver(request);
            return HandleResult(response);
        }

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
        Summary = "Delete A Bus Driver By Admin Endpoint",
        Description = "This endpoint deletes a driver by admin account. It requires admin privelege",
        OperationId = "busdriver.deleteByAdmin",
        Tags = new[] { "PersonaEndpoints" })
        ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete("delete-busdriver")]
        public async Task<ActionResult<BaseResponse>> DeleteBusdriverByAdminAsync(Guid busDriverId)
        {
            var response = await _busDriverSevice.DeleteBusdriver(busDriverId);
            return HandleResult(response);
        }

        [Authorize(Policy = AuthConstants.Policies.ADMINS)]
        [SwaggerOperation(
     Summary = "Edit Staff Endpoint",
     Description = "This endpoint edits an existing staff. It requires Admin privilege",
     OperationId = "staff.edit",
     Tags = new[] { "PersonaEndpoints" })
 ]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiResponse<StaffResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut("edit-staff")]
        public async Task<ActionResult<ApiResponse<StaffResponse>>> EditStaffAsync([FromBody] EditStaffRequest request)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.Id == request.StaffId);
            if (staff == null)
            {
                return HandleResult(new ApiResponse<StaffResponse> { Status = false, Message = "Staff not found" });
            }

            var persona = await _context.Users.FirstOrDefaultAsync(x => x.Id == staff.PersonaId);
            if (persona == null)
            {
                return HandleResult(new ApiResponse<StaffResponse> { Status = false, Message = "User account not found" });
            }

            // Update properties if they are not null or empty
            if (!string.IsNullOrEmpty(request.LastName))
            {
                staff.LastName = request.LastName;
                persona.LastName = request.LastName;
            }

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                staff.FirstName = request.FirstName;
                persona.FirstName = request.FirstName;
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                staff.PhoneNumber = request.PhoneNumber;
            }

            // add check for middle name
            if (!string.IsNullOrEmpty(request.MiddleName))
            {
                staff.MiddleName = request.MiddleName;
                persona.MiddleName = request.MiddleName;
            }

            // Update JobTitleId and DepartmentId if they are provided
            if (request.JobTitleId.HasValue)
            {
                staff.JobTitleId = request.JobTitleId;
            }

            if (request.DepartmentId.HasValue)
            {
                staff.DepartmentId = request.DepartmentId;
            }

            _context.Staffs.Update(staff);
            _context.Users.Update(persona);
            await _context.SaveChangesAsync();

            var response = new StaffResponse
            {
                StaffId = staff.Id,
                LastName = staff.LastName,
                FirstName = staff.FirstName,
                PhotoUrl = staff.PhotoUrl,
                MiddleName = staff.MiddleName,
                PhoneNumber = staff.PhoneNumber,
                JobTitleId = staff.JobTitleId,
                DepartmentId = staff.DepartmentId
            };

            return HandleResult(new ApiResponse<StaffResponse> { Data = response, Status = true });
        }

    }


}