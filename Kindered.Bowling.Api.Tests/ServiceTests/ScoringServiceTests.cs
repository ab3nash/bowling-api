using Kindred.Bowling.Api.Models;
using Kindred.Bowling.Api.Services.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Kindred.Bowling.Api.Tests.ServiceTests
{
    public class ScoringServiceTests
    {
        #region Empty Frames
        [Fact]
        public void CalculateScore_NullFrame_EmptyScoring()
        {
            ScoringService scoringService = new ScoringService();

            ScoringResultDto result = scoringService.CalculateScore(null);

            Assert.Empty(result.FrameProgressScores);

        }

        [Fact]
        public void CalculateScore_NullFrame_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();

            ScoringResultDto result = scoringService.CalculateScore(null);

            Assert.False(result.GameCompleted);

        }

        [Fact]
        public void CalculateScore_EmptyFrame_EmptyScoring()
        {
            ScoringService scoringService = new ScoringService();

            ScoringResultDto result = scoringService.CalculateScore(new List<Frame>());

            Assert.Empty(result.FrameProgressScores);

        }

        [Fact]
        public void CalculateScore_EmptyFrame_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();

            ScoringResultDto result = scoringService.CalculateScore(new List<Frame>());

            Assert.False(result.GameCompleted);

        }
        #endregion

        #region Single Incomplete Frame
        [Fact]
        public void CalculateScore_SingleIncompleteFrame_EmptyScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(2, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Single(result.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleIncompleteFrame_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(2, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal("*", result.FrameProgressScores[0]);
        }

        [Fact]
        public void CalculateScore_SingleIncompleteFrame_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(2, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Single Open Frame
        [Fact]
        public void CalculateScore_SingleOpenFrame_SingleFrameProgressScores()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(2, 6) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Single(result.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleOpenFrame_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(2, 6) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal("8", result.FrameProgressScores[0]);
        }

        [Fact]
        public void CalculateScore_SingleOpenFrame_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(2, 6) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Single Spare Frame
        [Fact]
        public void CalculateScore_SingleSpareFrame_SingleFrameProgressScores()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(2, 8) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Single(result.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleSpareFrame_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(2, 8) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal("*", result.FrameProgressScores[0]);
        }

        [Fact]
        public void CalculateScore_SingleSpareFrame_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(2, 8) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Single Strike Frame
        [Fact]
        public void CalculateScore_SingleStrikeFrame_SingleFrameProgressScores()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(10, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Single(result.FrameProgressScores);
        }

        [Fact]
        public void CalculateScore_SingleStrikeFrame_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(10, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal("*", result.FrameProgressScores[0]);
        }

        [Fact]
        public void CalculateScore_SingleStrikeFrame_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(10, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Incomplete Last Frame
        [Fact]
        public void CalculateScore_IncompleteLastFrame_CorrectFrameProgressScoresCount()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(3, 5), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(2, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_IncompleteLastFrame_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(3, 5), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "16", "*" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_IncompleteLastFrame_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(3, 5), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Incomplete Frame After Strike
        [Fact]
        public void CalculateScore_IncompleteFrameAfterStrike_CorrectFrameProgressScoresCount()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(3, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_IncompleteFrameAfterStrike_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "*", "*" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_IncompleteFrameAfterStrike_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Complete Frame After Strike
        [Fact]
        public void CalculateScore_CompleteFrameAfterStrike_CorrectFrameProgressScoresCount()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(3, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_CompleteFrameAfterStrike_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "27", "36" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_CompleteFrameAfterStrike_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(10, null), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Incomplete Frame After Spare
        [Fact]
        public void CalculateScore_IncompleteFrameAfterSpare_CorrectFrameProgressScoresCount()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(3, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_IncompleteFrameAfterSpare_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "24", "*" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_IncompleteFrameAfterSpare_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, null) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Complete Frame After Spare
        [Fact]
        public void CalculateScore_CompleteFrameAfterSpare_CorrectFrameProgressScoresCount()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(3, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_CompleteFrameAfterSpare_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid = new List<string> { "8", "24", "33" }.SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_CompleteFrameAfterSpare_IncompleteGame()
        {
            ScoringService scoringService = new ScoringService();
            var frames = new List<Frame> { new Frame(1, 7), new Frame(9, 1), new Frame(6, 3) };

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.False(result.GameCompleted);
        }
        #endregion

        #region Complete Games
        [Fact]
        public void CalculateScore_CompleteGame_CorrectFrameProgressScoresCount()
        {
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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

            Assert.Equal(10, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeIncomplete_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
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
            bool isResultValid =
                new List<string> { "8", "26", "34", "43", "52", "72", "88", "101", "107", "*" }
                .SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeIncomplete_IsInompleteGame()
        {
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
                new Frame(8, 1)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.Equal(10, result.FrameProgressScores.Count);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeComplete_CorrectFrameProgressScoring()
        {
            ScoringService scoringService = new ScoringService();
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
                new Frame(8, 1)};

            ScoringResultDto result = scoringService.CalculateScore(frames);
            bool isResultValid =
                new List<string> { "8", "26", "34", "43", "52", "72", "88", "101", "107", "126" }
                .SequenceEqual(result.FrameProgressScores);

            Assert.True(isResultValid);
        }

        [Fact]
        public void CalculateScore_BonusFrameAfterStrikeComplete_IsInompleteGame()
        {
            ScoringService scoringService = new ScoringService();
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
                new Frame(8, 1)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.True(result.GameCompleted);
        }
        #endregion
        #region BonusThrowAfterSpareAbsent
        [Fact]
        public void CalculateScore_BonusThrowAfterSpareAbsent_CorrectFrameProgressScoresCount()
        {
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
            ScoringService scoringService = new ScoringService();
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
                new Frame(8, null)};

            ScoringResultDto result = scoringService.CalculateScore(frames);

            Assert.True(result.GameCompleted);
        }
        #endregion
        #region After Spare
        #endregion
        #endregion
    }
}
