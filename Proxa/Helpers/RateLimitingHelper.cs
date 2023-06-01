using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Proxa.Helpers
{
    public class RateLimitingHelper
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingHelper> _logger;
        private readonly RateLimitingOptions _options;

        public RateLimitingHelper(RequestDelegate next, ILogger<RateLimitingHelper> logger, IOptions<RateLimitingOptions> options)
        {
            _next = next;
            _logger = logger;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var clientId = context.Request.Headers["X-Client-Id"].ToString();
            if (string.IsNullOrEmpty(clientId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Client ID is missing.");
                return;
            }

            if (!IsAllowed(clientId))
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Rate limit exceeded.");
                return;
            }

            await _next(context);
        }

        private bool IsAllowed(string clientId)
        {
            // Implement your rate limiting logic here.
            // You can use the _options object to access the rate limiting settings.
            // For example, you can check the number of requests made by the client in the last minute and compare it with the limit.
            // If the limit is exceeded, return false; otherwise, return true.

            return true;
        }
    }

    public class RateLimitingOptions
    {
        public int RequestsPerMinute { get; set; }
    }
}
