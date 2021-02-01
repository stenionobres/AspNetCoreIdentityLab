using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Collections.Generic;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityLab.Api.Jwt
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;

        public JwtService(IConfiguration configuration, IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory)
        {
            _configuration = configuration;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userClaims = _userClaimsPrincipalFactory.CreateAsync(user).Result;

            foreach (var userClaim in userClaims.Claims)
            {
                var addedClaim = claims.Find(c => c.Type.Equals(userClaim.Type) && c.Value.Equals(userClaim.Value));

                if (addedClaim == null)
                {
                    claims.Add(new Claim(userClaim.Type, userClaim.Value));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpirationInDays"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
