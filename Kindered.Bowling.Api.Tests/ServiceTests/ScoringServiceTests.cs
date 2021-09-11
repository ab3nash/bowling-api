using Kindred.Bowling.Api.Models;
using Kindred.Bowling.Api.Services.Scoring;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Kindred.Bowling.Api.Tests.ServiceTests
{
    public class ScoringServiceTests
    {
        [Fact]
        public void CalculateScore_PinsDownedNull_ReturnsNotNull()
        {
            ScoringService scoringService = CreateDefaultScoringService();

            ScoringResultDto scoringResult = scoringService.CalculateScore(null);

            Assert.NotNull(scoringResult);
        }

        [Fact]
        public void CalculateScore_PinsDownedEmpty_ReturnsNotNull()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int>();

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.NotNull(scoringResult);
        }

        [Fact]
        public void CalculateScore_IncompleteFirstFrame_ReturnsEmptyFrameProgressScores()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 1 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.Empty(scoringResult.FrameProgressScores);
        }

        //[Fact]
        //public void CalculateScore_IncompleteFirstFrame_GameNotComplete()
        //{
        //    ScoringService scoringService = CreateDefaultScoringService();
        //    var pinsDowned = new List<int> { 1 };

        //    ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

        //    Assert.False(scoringResult.GameCompleted);
        //}

        [Fact]
        public void CalculateScore_SingleOpenFrame_ReturnsSingleFrameProgressScores()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 1, 1 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.Single(scoringResult.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleOpenFrame_ReturnsCorrectFrameProgressScores()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 1, 1 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);
            string currentFrameProgressScore = scoringResult.FrameProgressScores[0];

            Assert.Equal("2", currentFrameProgressScore);
        }

        [Fact]
        public void CalculateScore_SingleOpenFrame_GameNotComplete()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 1, 1 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.False(scoringResult.GameCompleted);
        }

        [Fact]
        public void CalculateScore_TwoOpenFrames_ReturnsTwoFrameProgressScores()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 1, 1, 2, 2 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.Equal(2, scoringResult.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_TwoOpenFrames_GameNotComplete()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 1, 1, 2, 2 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.False(scoringResult.GameCompleted);
        }

        [Fact]
        public void CalculateScore_SingleStrikeFrame_ReturnsSingleFrameProgressScores()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 10 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.Single(scoringResult.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleStrikeFrame_GameNotComplete()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 1, 1 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.False(scoringResult.GameCompleted);
        }

        [Fact]
        public void CalculateScore_OpenAfterStrike_ReturnsTwoFrameProgressScores()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 10, 1, 2 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.Equal(2, scoringResult.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_OpenAfterStrike_ReturnsCorrectFirstFrameProgressScores()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 10, 1, 2 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.Equal(2, scoringResult.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_OpenAfterStrike_GameNotComplete()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 10, 1, 2 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.False(scoringResult.GameCompleted);
        }

        [Fact]
        public void CalculateScore_CompleteGame_GameNotComplete()
        {
            ScoringService scoringService = CreateDefaultScoringService();
            var pinsDowned = new List<int> { 10, 1, 2 };

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.False(scoringResult.GameCompleted);
        }


        private ScoringService CreateDefaultScoringService()
        {
            return new ScoringService();
        }

        [Theory, MemberData(nameof(GamesListToValidateScoring))]
        public void CalculateScore_TwoOpenFrames_ReturnCorrectFrameProgressScores(List<int> pinsDowned, List<string> expectedScore)
        {
            ScoringService scoringService = CreateDefaultScoringService();

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);
            bool isCorrectFrameProgressScore = expectedScore.SequenceEqual(scoringResult.FrameProgressScores);

            Assert.True(isCorrectFrameProgressScore);
        }

        [Theory, MemberData(nameof(GamesListToCheckCompletion))]
        public void CalculateScore_TwoOpenFrames_ReturnCorrectGameCompletionStatus(List<int> pinsDowned, bool expectedStatus)
        {
            ScoringService scoringService = CreateDefaultScoringService();

            ScoringResultDto scoringResult = scoringService.CalculateScore(pinsDowned);

            Assert.Equal(expectedStatus, scoringResult.GameCompleted);
        }

        public static IEnumerable<object[]> GamesListToValidateScoring => new List<object[]>  {
            new object[] { null, new List<string>() },
            new object[] { new List<int>(), new List<string>() },
            new object[] { new List<int>{ 1, 1 }, new List<string> {"2"} },
            new object[] { new List<int> { 2, 2 }, new List<string> {"4"} },
            new object[] { new List<int> { 5, 4 }, new List<string> {"9"} },
            new object[] { new List<int> { 10 }, new List<string>() },
            new object[] { new List<int> { 10, 1 }, new List<string>() },
            new object[] { new List<int> { 10, 1, 2 }, new List<string> {"13", "16"} },
            new object[] { new List<int> { 7, 3 }, new List<string> () },
            new object[] { new List<int> { 7, 3, 4 }, new List<string> {"14"} },
            new object[] { new List<int> { 7, 3, 4, 2 }, new List<string> {"14", "20"} },
            new object[] { new List<int> { 10, 10 }, new List<string> () },
            new object[] { new List<int> { 10, 10, 10 }, new List<string> { "30"} },
            new object[] {
                new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 },
                new List<string> { "30, 60, 90, 120, 150, 180, 210, 240, 270, 300"} },
        };

        public static IEnumerable<object[]> GamesListToCheckCompletion => new List<object[]>  {
            new object[] { null, false},
            new object[] { new List<int>(), false },
            new object[] { new List<int> { 1 }, false },
            new object[] { new List<int> { 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1, 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false },
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false},
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false},
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false},
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false},
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false},
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false},
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false},
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false},
            new object[] { new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, true},
            new object[] { new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, true }
        };
    }
}
