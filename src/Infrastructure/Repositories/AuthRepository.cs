using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Responses;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;
        public AuthRepository(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            IConfiguration configuration,
            AppDbContext dataContex
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _userManager = userManager;
            _dbContext = dataContex;
        }
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<ActionResult<User>> ShowUserByIdAsync(string Id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == Id);
            return user;
        }
       
        public async Task<ActionResult<User>> ShowUserByEmailAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == email);
            return user;
        }

        public async Task<ActionResult<AuthenticateResponse>> RegisterUserAsync(CreateUserDTO request)
        {
            if (request == null)
            {
               throw new NotFoundException($"Request body cannot be empty.");
            }
            var newTenant = new Tenant()
            {
                Id = request.SchoolAlias,
                Name = request.SchoolAlias,
                TenantAdminEmail = request.Email
            };

            var newUser = new User() 
            {
                Email = request.Email,
                UserName = request.SchoolAlias,
                // TenantId = request.SchoolAlias,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                SchoolName = request.SchoolName,
                C_Password = request.C_Password,
                CreatedAt = DateTime.UtcNow,
            };
            if (request.Password != request.C_Password)
            {
                throw new BadRequestException($"Password and confirm password mismatch.");
            }
            var passwordValidator = _userManager.PasswordValidators.FirstOrDefault();
            if (passwordValidator != null)
            {
                var validationResult = await passwordValidator.ValidateAsync(_userManager, newUser, request.Password);
                
                if (!validationResult.Succeeded)
                {
                    throw new BadRequestException(
                        "Passwords must be at least 6 characters Passwords must have at least one lowercase ('a'-'z') Passwords must have at least one uppercase ('A'-'Z') Passwords must have at least one digit ('0'-'9') Passwords must have at least one non alphanumeric character"
                    );
                }
            }
            
            var userByEmail = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (userByEmail != null)
            {
               throw new BadRequestException($"User email already exist.");
            }
            var userByUsername = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == request.SchoolAlias);
            if (userByUsername != null)
            {
               throw new BadRequestException($"User school alias already exist.");
            }
            
            var tenantResult = await _dbContext.Tenants.AddAsync(newTenant);
            await _dbContext.SaveChangesAsync();

            var newTenantId = tenantResult.Entity.Id;
            newUser.TenantId = newTenantId;

            var result = await _userManager.CreateAsync(newUser, request.Password);
            
            var token = GenerateNewJwtToken(newUser, request.SchoolAlias);
            return new AuthenticateResponse(newUser, token);
            
        }
        public async Task<AuthenticateResponse?> Login(AuthenticateRequest request)
        {
            if (request == null)
            {
               throw new NotFoundException($"Request body cannot be empty.");
            }
            // throw new BadRequestException(request.Email);
            var userByEmail = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == request.Email) ?? throw new BadRequestException($"User does not exist.");
            var isCorrectPass = await _userManager.CheckPasswordAsync(userByEmail, request.Password);
            if (!isCorrectPass)
            {
               throw new BadRequestException($"User password mismatch.");
            }
            // var token = GenerateJwtToken(userByEmail);
            var token = GenerateNewJwtToken(userByEmail, userByEmail.UserName);
            // throw new BadRequestException(userByEmail.UserName);
            return new AuthenticateResponse(userByEmail, token);
        }

        private string GenerateNewJwtToken(User user, string tenantId)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("id", user.Id),
                    new Claim("TenantId", tenantId),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? ""),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
            // var tokenHandler = new JwtSecurityTokenHandler();
            // var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(new[] 
            //     { 
            //         new("id", user.Id),
            //         new Claim("TenantId", tenantId),
            //     }),
            //     Expires = DateTime.UtcNow.AddDays(7),
            //     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            // };
            // var token = tokenHandler.CreateToken(tokenDescriptor);
            // return tokenHandler.WriteToken(token);


            // var claims = new List<Claim>
            // {
            //     new("id", user.Id),
            //     new("TenantId", tenantId),
            //     new(JwtRegisteredClaimNames.Sub, user.Email ?? ""),
            //     new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //     new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            // };
            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // var token = new JwtSecurityToken(
            //     issuer: _configuration["Jwt:Issuer"],
            //     audience: _configuration["Jwt:Audience"],
            //     claims: claims,
            //     expires: DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
            //     signingCredentials: creds
            // );
            // return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new("id", user.Id),
                new(JwtRegisteredClaimNames.Sub, user.Email ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}