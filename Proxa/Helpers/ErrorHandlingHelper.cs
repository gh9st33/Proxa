using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Proxa.Helpers
{
    public class ErrorHandlingHelper
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingHelper> _logger;

        public ErrorHandlingHelper(RequestDelegate next, ILogger<ErrorHandlingHelper> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async System.Threading.Tasks.Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new
            {
                error = new
                {
                    message = exception.Message,
                    type = exception.GetType().Name,
                    details = exception.StackTrace
                }
            });

            await context.Response.WriteAsync(result);
        }
    }
}
