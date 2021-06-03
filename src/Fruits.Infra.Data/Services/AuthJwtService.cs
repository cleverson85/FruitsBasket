using Fruits.Domain.Interfaces.Services;
using Fruits.Domain.Models;
using Fruits.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fruits.Infra.Data.Services
{
    public class AuthJwtService : IAuthJwtService
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public AuthJwtService(IUserService userService, AppSettings appSettings)
        {
            _userService = userService;
            _appSettings = appSettings;
        }

        private async Task<SigningCredentials> GetSigningCredentials(string securityKey)
        {
            var key = Encoding.UTF8.GetBytes(securityKey);
            return await Task.FromResult(new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
        }

        private async Task<JwtSecurityToken> GenerateTokenOptions(SigningCredentials signingCredentials)
        {
            return await Task.FromResult
                (
                    new JwtSecurityToken(
                            issuer: _appSettings.JWTSettings.ValidIssuer,
                            audience: _appSettings.JWTSettings.ValidAudience,
                            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_appSettings.JWTSettings.ExpiryInMinutes)),
                            signingCredentials: signingCredentials
                        )
                );
        }

        private async Task<SecurityToken> GenerateTokenOptions(SigningCredentials signingCredentials, User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_appSettings.JWTSettings.ExpiryInMinutes)),
                SigningCredentials = signingCredentials
            };

            return await Task.FromResult(new JwtSecurityTokenHandler().CreateToken(tokenDescriptor));
        }

        public async Task<string> GenerateToken(User user)
        {
            var signingCredentials = await GetSigningCredentials(_appSettings.JWTSettings.SecurityKey);
            var tokenDescriptor = await GenerateTokenOptions(signingCredentials, user);
            var token = await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokenDescriptor));

            return token;
        }
    }
}


