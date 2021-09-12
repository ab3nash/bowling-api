using Kindred.Bowling.Api.Services.Framing;
using Kindred.Bowling.Api.Services.Scoring;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Kindred.Bowling.Api.Tests.CompositionRootTests
{
    public class StartupTests
    {
        private readonly ServiceProvider _serviceProvider;

        public StartupTests()
        {
            Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();
            var services = new ServiceCollection();
            Startup startup = new Startup(configurationStub.Object);
            startup.ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void Constructor_SetConfiguration_ConfigurationIsSet()
        {
            Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();
            var services = new ServiceCollection();
            Startup startup = new Startup(configurationStub.Object);

            startup.ConfigureServices(services);

            Assert.NotNull(startup.Configuration);
        }

        [Fact]
        public void ConfigureServices_RegistersScoringServiceCorrectly()
        {
            var service = _serviceProvider.GetRequiredService<IScoringService>();

            Assert.NotNull(service);
            Assert.IsType<TraditionalScoringService>(service);
        }

        [Fact]
        public void ConfigureServices_RegistersFramingServiceCorrectly()
        {
            var service = _serviceProvider.GetRequiredService<IFramingService>();

            Assert.NotNull(service);
            Assert.IsType<FramingService>(service);
        }
    }
}
