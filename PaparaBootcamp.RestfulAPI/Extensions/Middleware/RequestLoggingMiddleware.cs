using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PaparaBootcamp.RestfulAPI.Extensions.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //_logger.LogInformation("Handling request: " + context.Request.Path);
            _logger.LogInformation($"Gelen istek: {context.Request.Method} {context.Request.Path}");

            await _next(context);
            _logger.LogInformation($"Giden yanit: {context.Response.StatusCode}");
        }
    }

}

