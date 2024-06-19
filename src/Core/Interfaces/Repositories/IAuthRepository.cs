using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests;
using Shared.Models.Responses;
using UserService.Models;
using UserService.Models.RequestBody;

namespace Core.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<ApiResponse<List<UserResponse>>> GetAllUsersAsync();
        // Task<bool> UserExists(string email);
        Task<ApiResponse<UserResponse>> ShowUserByIdAsync(string id);
        // Task<ActionResult<Tenant>> ShowTenantBySchoolAliasAsync(string schoolAlias);
        Task<ApiResponse<UserResponse>> ShowUserByEmailAsync(string email);
        Task<ApiResponse<AuthenticateResponse>> RegisterUserAsync(CreateUserDTO user);
        Task<ApiResponse<AuthenticateResponse>> Login(AuthenticateRequest model);
        // Task<string> GetJwtResponse();

        // Task<ApiResponse<UserResponse>> TestUserAsyn(AuthenticateRequest model);
    }
}