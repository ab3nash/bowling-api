using Kindred.Bowling.Api.Models;
using Kindred.Bowling.Api.Services.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Kindred.Bowling.Api.Tests.ServiceTests
{
    public class TraditionalScoringServiceTests
    {
        #region Empty Frames
        [Fact]
        public void CalculateScore_NullFrame_EmptyScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();

            ScoringResultDto result = scoringService.CalculateScore(null);

            Assert.Empty(result.FrameProgressScores);

        }

        [Fact]
        public void CalculateScore_NullFrame_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();

            ScoringResultDto result = scoringService.CalculateScore(null);

            Assert.False(result.GameCompleted);

        }

        [Fact]
        public void CalculateScore_EmptyFrame_EmptyScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();

            ScoringResultDto result = scoringService.CalculateScore(new List<Frame>());

            Assert.Empty(result.FrameProgressScores);

        }

        [Fact]
        public void CalculateScore_EmptyFrame_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();

            ScoringResultDto result = scoringService.CalculateScore(new List<Frame>());

            Assert.False(result.GameCompleted);

        }
        #endregion

        #region Single Incomplete Frame
        [Fact]
        public void CalculateScore_SingleIncompleteFrame_EmptyScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(2, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Single(result.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleIncompleteFrame_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(2, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal("*", result.FrameProgressScores[0]);
        }

        [Fact]
        public void CalculateScore_SingleIncompleteFrame_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(2, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Single Open Frame
        [Fact]
        public void CalculateScore_SingleOpenFrame_SingleFrameProgressScores()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(2, 6) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Single(result.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleOpenFrame_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(2, 6) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal("8", result.FrameProgressScores[0]);
        }

        [Fact]
        public void CalculateScore_SingleOpenFrame_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(2, 6) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Single Spare Frame
        [Fact]
        public void CalculateScore_SingleSpareFrame_SingleFrameProgressScores()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(2, 8) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Single(result.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleSpareFrame_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(2, 8) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal("*", result.FrameProgressScores[0]);
        }

        [Fact]
        public void CalculateScore_SingleSpareFrame_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(2, 8) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Single Strike Frame
        [Fact]
        public void CalculateScore_SingleStrikeFrame_SingleFrameProgressScores()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(10, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Single(result.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleStrikeFrame_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(10, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal("*", result.FrameProgressScores[0]);
        }

        [Fact]
        public void CalculateScore_SingleStrikeFrame_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(10, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Incomplete Last Frame
        [Fact]
        public void CalculateScore_IncompleteLastFrame_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(3, 5), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(3, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_IncompleteLastFrame_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(3, 5), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "16", "*" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_IncompleteLastFrame_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(3, 5), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Incomplete Frame After Strike
        [Fact]
        public void CalculateScore_IncompleteFrameAfterStrike_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(3, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_IncompleteFrameAfterStrike_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "*", "*" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_IncompleteFrameAfterStrike_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Complete Frame After Strike
        [Fact]
        public void CalculateScore_CompleteFrameAfterStrike_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(3, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_CompleteFrameAfterStrike_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "27", "36" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_CompleteFrameAfterStrike_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Incomplete Frame After Spare
        [Fact]
        public void CalculateScore_IncompleteFrameAfterSpare_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(3, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_IncompleteFrameAfterSpare_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "24", "*" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_IncompleteFrameAfterSpare_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Complete Frame After Spare
        [Fact]
        public void CalculateScore_CompleteFrameAfterSpare_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(3, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_CompleteFrameAfterSpare_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "24", "33" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_CompleteFrameAfterSpare_IncompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Complete Games
        [Fact]
        public void CalculateScore_CompleteGame_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(6, 2) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(10, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_CompleteGame_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(6, 2) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid =
                new List<string> { "8", "26", "34", "43", "52", "72", "88", "101", "107", "115" }
                .SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_CompleteGame_IsCompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(6, 2) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.True(result.GameCompleted);
        }
        #endregion

        #region Perfect Games
        [Fact]
        public void CalculateScore_PerfectGame_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, 10, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(10, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_PerfectGame_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, 10, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid =
                new List<string> { "30", "60", "90", "120", "150", "180", "210", "240", "270", "300" }
                .SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_PerfectGame_IsPerfectGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, null),
                new Frame(10, 10, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.True(result.GameCompleted);
        }
        #endregion

        #region Games with Bonus Frames
        #region BonusFrameAfterStrikeAbsent
        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeAbsent_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(10, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(10, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeAbsent_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(10, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid =
                new List<string> { "8", "26", "34", "43", "52", "72", "88", "101", "107", "*" }
                .SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeAbsent_IsInompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(10, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion
        #region BonusFrameAfterStrikeIncomplete
        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeIncomplete_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(10, null),
                new Frame(8, null, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(10, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeIncomplete_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(10, null),
                new Frame(8, null, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid =
                new List<string> { "8", "26", "34", "43", "52", "72", "88", "101", "107", "*" }
                .SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeIncomplete_IsInompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(10, null),
                new Frame(8, null)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion
        #region BonusFrameAfterStrikeComplete
        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeComplete_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(10, null),
                new Frame(8, 1, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(10, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeComplete_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(10, null),
                new Frame(8, 1, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid =
                new List<string> { "8", "26", "34", "43", "52", "72", "88", "101", "107", "126" }
                .SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeComplete_IsCompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(10, null),
                new Frame(8, 1, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.True(result.GameCompleted);
        }
        #endregion
        #region BonusThrowAfterSpareAbsent
        [Fact]
        public void CalculateScore_BonusThrowAfterSpareAbsent_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(8, 2) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(10, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_BonusThrowAfterSpareAbsent_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(8, 2) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid =
                new List<string> { "8", "26", "34", "43", "52", "72", "88", "101", "107", "*" }
                .SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_BonusThrowAfterSpareAbsent_IsInompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(8, 2) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion
        #region BonusThrowAfterSparePresent
        [Fact]
        public void CalculateScore_BonusThrowAfterSparePresent_CorrectFrameProgressScoresCount()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(8, 2),
                new Frame(8, null, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(10, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_BonusThrowAfterSparePresent_CorrectFrameProgressScoring()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(8, 2),
                new Frame(8, null, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid =
                new List<string> { "8", "26", "34", "43", "52", "72", "88", "101", "107", "125" }
                .SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_BonusThrowAfterSparePresent_IsInompleteGame()
        {
            TraditionalScoringService scoringService = new TraditionalScoringService();
            var frames = new List<Frame> {
                new Frame(1, 7),
                new Frame(10, null),
                new Frame(1, 7),
                new Frame(2, 7),
                new Frame(8, 1),
                new Frame(10, null),
                new Frame(8, 2),
                new Frame(6, 4),
                new Frame(3, 3),
                new Frame(8, 2),
                new Frame(8, null, true)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.True(result.GameCompleted);
        }
        #endregion
        #region After Spare
        #endregion
        #endregion
    }
}
