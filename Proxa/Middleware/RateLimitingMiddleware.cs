using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Proxa.Helpers;

namespace Proxa.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private readonly RateLimitingHelper _rateLimitingHelper;

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger, RateLimitingHelper rateLimitingHelper)
        {
            _next = next;
            _logger = logger;
            _rateLimitingHelper = rateLimitingHelper;
        }

        public async Task Invoke(HttpContext context)
        {
            await _rateLimitingHelper.Invoke(context);
            await _next(context);
        }
    }
}
