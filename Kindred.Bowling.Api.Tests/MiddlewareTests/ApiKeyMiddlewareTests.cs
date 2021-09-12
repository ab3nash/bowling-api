using Kindred.Bowling.Api.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Kindred.Bowling.Api.Tests.MiddlewareTests
{
    public class ApiKeyMiddlewareTests
    {
        private readonly ApiKeyMiddleware _apiKeyMiddleware;
        private const string TestAPIKey = "4acf8c65-6dce-4f90-82e9-aa36e9489e49";
        private const string ApiKeyHeaderName = "ApiKey";
        public ApiKeyMiddlewareTests()
        {
            // Fake request delegate
            var requestDelegate = new RequestDelegate(
                (innerContext) => Task.FromResult(0));

            // Substitute logger
            var nullLogger = new NullLogger<ApiKeyMiddleware>();

            // Mock IConfiguration
            var inMemorySettings = new Dictionary<string, string> {
                {ApiKeyHeaderName, TestAPIKey}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _apiKeyMiddleware = new ApiKeyMiddleware(requestDelegate, nullLogger, configuration);
        }

        [Fact]
        public async Task InvokeAsync_NoAPIKey_401UnauthorizedResponse()
        {
            // setup
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            // act
            await _apiKeyMiddleware.InvokeAsync(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(httpContext.Response.Body);
            var responseText = reader.ReadToEnd();

            // Assert 
            // No ApiKey header => 401 Unauthorized
            Assert.Equal(StatusCodes.Status401Unauthorized, httpContext.Response.StatusCode);
            Assert.Equal("API Key is required to authorize.", responseText);
        }

        [Fact]
        public async Task InvokeAsync_IncorrectAPIKey_401UnauthorizedResponse()
        {
            // setup
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers[ApiKeyHeaderName] = "IncorrectKey";
            httpContext.Response.Body = new MemoryStream();

            // act
            await _apiKeyMiddleware.InvokeAsync(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(httpContext.Response.Body);
            var responseText = reader.ReadToEnd();

            // Assert 
            // Incorrect ApiKey header => 401 Unauthorized
            Assert.Equal(StatusCodes.Status401Unauthorized, httpContext.Response.StatusCode);
            Assert.Equal("Unauthorized client - invalid API Key.", responseText);
        }

        [Fact]
        public async Task InvokeAsync_CorrectAPIKey_200SuccessResponse()
        {
            // setup
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers[ApiKeyHeaderName] = TestAPIKey;

            // act
            await _apiKeyMiddleware.InvokeAsync(httpContext);

            // Assert 
            // Incorrect ApiKey header => 401 Unauthorized
            Assert.Equal(StatusCodes.Status200OK, httpContext.Response.StatusCode);
        }
    }
}
