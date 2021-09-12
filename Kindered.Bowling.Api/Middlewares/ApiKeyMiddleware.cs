using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Kindred.Bowling.Api.Middlewares
{
    /// <summary>
    /// Middleware to handle API key authentication
    /// Expects API Key in ApiKey header
    /// Based on: https://github.com/aram87/SecuringWebApiUsingApiKey
    /// </summary>
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "ApiKey";
        private readonly ILogger<ApiKeyMiddleware> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Creates an instance of the ApiMiddleware Class
        /// </summary>
        /// <param name="next">The RequestDelegate to build the request pipeline</param>
        /// <param name="logger">The configured Logger</param>
        /// <param name="configuration">Application Configuration</param>
        public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Validates API key in the request header to API key in the app configuration file
        /// </summary>
        /// <param name="context">The HttpContext for the request</param>
        /// <returns>
        /// Task that represents the execution of the middleware with
        /// HTTP 401 - Unauthorized response if API key is not provided or is invalid
        /// or continues the execution of request pipeline
        /// </returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key is required to authorize.");
                _logger.LogDebug("API Key not provided.");
                return;
            }

            var apiKey = _configuration.GetValue<string>(ApiKeyHeaderName);

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized client - invalid API Key.");
                _logger.LogDebug("Unauthorized client - invalid API Key.");
                return;
            }

            _logger.LogDebug("Client authorised with API Key auth.");
            await _next(context);
        }
    }
}
