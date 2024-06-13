﻿using static Shared.Constants.StringConstants;
using System.Security.Claims;
using System.Text;
using Core.Interfaces.Services;
using Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services
{
    public class TokenService : ITokenService
    {
        public TokenResult GetToken(PersonaResponse persona)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? DefaultValues.JWT_SECRET_KEY);

            var claims = new List<Claim>
            {
                new Claim("id", persona.Id.ToString()),
                new Claim(ClaimTypes.Email, persona.Email),
                new Claim(ClaimTypes.Name, persona.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("fullName",persona.FirstName +" "+ persona.LastName),
                //new Claim("tenantKey",persona.TenantKey),
            };

            foreach (var role in persona.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(30),
                //Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return new(jwtToken, tokenDescriptor.Expires.Value);
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }
    }

}
