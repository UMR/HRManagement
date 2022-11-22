using HRManagement.Application.Contracts;
using HRManagement.Application.Dtos.Tokens;
using HRManagement.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRManagement.Infrastructure.Services
{
    public class TokenGenerator: ITokenGenerator
    {
        public TokenResult GenerateToken(User user, string secret, string issuer, string audience)
        {
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("Id", user.Id.ToString()),
                };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);            
            string token = tokenHandler.WriteToken(securityToken);

            return new TokenResult { Id = securityToken.Id, Token = token };
        }
    }
}
