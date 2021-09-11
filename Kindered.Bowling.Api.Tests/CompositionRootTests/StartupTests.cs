using Kindered.Bowling.Api.Services.Scoring;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Kindered.Bowling.Api.Tests.CompositionRootTests
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
        public void ConfigureServices_RegistersCarRepositoryCorrectly()
        {
            var service = _serviceProvider.GetRequiredService<IScoringService>();

            Assert.NotNull(service);
            Assert.IsType<ScoringService>(service);
        }
    }
}
