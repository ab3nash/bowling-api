using Kindred.Bowling.Api.Models;
using Xunit;

namespace Kindred.Bowling.Api.Tests.ModelTests
{
    public class ScoringRequestDtoTests
    {
        [Fact]
        public void Constructor_ObjectIsInitialized()
        {
            ScoringRequestDto scoringRequestDto = new ScoringRequestDto();
            Assert.NotNull(scoringRequestDto);
        }

        [Fact]
        public void Constructor_PinsDownedIsNotInitialized()
        {
            ScoringRequestDto scoringRequestDto = new ScoringRequestDto();
            Assert.Null(scoringRequestDto.PinsDowned);
        }
    }
}
