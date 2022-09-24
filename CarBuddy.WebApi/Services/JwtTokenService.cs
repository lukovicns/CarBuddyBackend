using CarBuddy.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace CarBuddy.WebApi.Services
{
    public class JwtTokenService
    {
        private readonly JwtTokenValidator _jwtTokenValidator;

        public JwtTokenService(JwtTokenValidator jwtTokenValidator) => _jwtTokenValidator = jwtTokenValidator;

        public Guid GetUserId(HttpRequest request)
        {
            var authorizationHeader = request.Headers["Authorization"].ToString();
            var token = _jwtTokenValidator.ValidateToken(authorizationHeader[7..]);

            return Guid.Parse(token?.Claims.First(x => x.Type == "sub").Value);
        }
    }
}
