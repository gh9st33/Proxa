using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Proxa.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Proxa.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthHelper _authHelper;

        public AuthMiddleware(RequestDelegate next, IOptions<AuthSettings> authSettings)
        {
            _next = next;
            _authHelper = new AuthHelper(authSettings.Value.SecretKey, authSettings.Value.TokenExpirationMinutes);
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            try
            {
                var jwtToken = new JwtSecurityToken(token);
                var validationParameters = _authHelper.GetValidationParameters();
                new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);
            }
            catch (SecurityTokenException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token");
                return;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return;
            }

            await _next(context);
        }
    }
}
