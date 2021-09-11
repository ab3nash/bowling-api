using Kindered.Bowling.Api.Models;
using Xunit;

namespace Kindered.Bowling.Api.Tests.DTOTests
{
    public class ScoringResultDtoTests
    {
        [Fact]
        public void Constructor_ObjectIsInitialized()
        {
            ScoringResultDto scoringResultDto = new ScoringResultDto();
            Assert.NotNull(scoringResultDto);
        }

        [Fact]
        public void Constructor_FrameProgressResultIsInitialized()
        {
            ScoringResultDto scoringResultDto = new ScoringResultDto();
            Assert.NotNull(scoringResultDto.FrameProgressScores);
        }

        [Fact]
        public void Constructor_FrameProgressResultIsEmpty()
        {
            ScoringResultDto scoringResultDto = new ScoringResultDto();
            Assert.Empty(scoringResultDto.FrameProgressScores);
        }
    }
}
