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
        Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync();
        // Task<bool> UserExists(string email);
        Task<ActionResult<User>> ShowUserByIdAsync(string id);
        // Task<ActionResult<Tenant>> ShowTenantBySchoolAliasAsync(string schoolAlias);
        Task<ActionResult<User>> ShowUserByEmailAsync(string email);
        Task<ActionResult<AuthenticateResponse>> RegisterUserAsync(CreateUserDTO user);
        Task<AuthenticateResponse?> Login(AuthenticateRequest model);

        // Task<ActionResult<User>> TestUserAsyn(AuthenticateRequest model);
    }
}