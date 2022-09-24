using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CarBuddy.Identity
{
    public class JwtTokenValidator
    {
        private readonly JwtSecurityTokenHandler _handler;
        private readonly IConfiguration _configuration;

        public JwtTokenValidator(
            JwtSecurityTokenHandler handler,
            IConfiguration configuration)
        {
            _handler = handler;
            _configuration = configuration;
        }

        public JwtSecurityToken ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            try
            {
                _handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Issuer"],
                    IssuerSigningKey = securityKey
                }, out SecurityToken securityToken);

                return (JwtSecurityToken)securityToken;
            }
            catch
            {
                return null;
            }
        }
    }
}
