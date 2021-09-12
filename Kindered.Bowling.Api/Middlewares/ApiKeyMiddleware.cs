using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Kindred.Bowling.Api.Middlewares
{
    /// <summary>
    /// Middleware to handle API key authentication
    /// Expects API Key in ApiKey header
    /// </summary>
    public class ApiKeyMiddleware
    {
        public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger, IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
