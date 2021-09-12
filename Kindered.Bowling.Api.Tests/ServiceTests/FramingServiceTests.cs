using Kindred.Bowling.Api.Models;
using Kindred.Bowling.Api.Services.Framing;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Kindred.Bowling.Api.Tests.ServiceTests
{
    public class FramingServiceTests
    {
        [Fact]
        public void GetFrames_NullInput_EmptyFrames()
        {
            FramingService framingService = new FramingService();

            List<Frame> frames = framingService.GetFrames(null);

            Assert.Empty(frames);
        }

        [Fact]
        public void GetFrames_EmptyInput_EmptyFrames()
        {
            FramingService framingService = new FramingService();

            List<Frame> frames = framingService.GetFrames(new List<int>());

            Assert.Empty(frames);
        }

        [Fact]
        public void GetFrames_SingleThrow_SingleFrame()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);

            Assert.Single(frames);
        }

        [Fact]
        public void GetFrames_SingleThrow_FrameFirstThrowPinsDownedValid()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);

            Assert.Equal(1, frames[0].FirstThrowPinsDowned);
        }

        [Fact]
        public void GetFrames_SingleThrow_FrameSecondThrowPinsDownedNull()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);

            Assert.Null(frames[0].SecondThrowPinsDowned);
        }

        [Fact]
        public void GetFrames_SingleThrow_IncompleteFrame()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);

            Assert.True(frames[0].IsIncomplete);
        }

        [Fact]
        public void GetFrames_TwoThrows_SingleFrame()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1, 8 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);

            Assert.Single(frames);
        }

        [Fact]
        public void GetFrames_TwoThrows_CompleteFrame()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1, 8 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);

            Assert.False(frames[0].IsIncomplete);
        }

        [Fact]
        public void GetFrames_TwoThrows_FrameFirstThrowPinsDownedValid()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1, 8 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);

            Assert.Equal(1, frames[0].FirstThrowPinsDowned);
        }

        [Fact]
        public void GetFrames_TwoThrows_FrameSecondThrowPinsDownedValid()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1, 8 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);

            Assert.Equal(8, frames[0].SecondThrowPinsDowned);
        }

        [Fact]
        public void GetFrames_MultipleThrows_CompleteFrames()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1, 8, 2, 4, 10 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);
            bool hasIncompleteFrame = frames.Any(f => f.IsIncomplete);

            Assert.False(hasIncompleteFrame);
        }

        [Fact]
        public void GetFrames_MultipleThrows_1InCompleteFrame()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1, 8, 2, 4, 10, 1 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);
            List<Frame> incompleteFrames = frames.Where(f => f.IsIncomplete).ToList();

            Assert.Single(incompleteFrames);
        }

        [Fact]
        public void GetFrames_PerfectGame_Has11Frames()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);

            Assert.Equal(11, frames.Count);
        }

        [Fact]
        public void GetFrames_PerfectGame_Has1BonusFrame()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);
            List<Frame> bonusFrames = frames.Where(f => f.IsBonus).ToList();

            Assert.Single(bonusFrames);
        }

        [Fact]
        public void GetFrames_PerfectGame_Has10StrikeFrames()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

            List<Frame> frames = framingService.GetFrames(pinsDowned);
            List<Frame> strikeFrames = frames.Where(f => f.IsStrike).ToList();

            Assert.Equal(10, strikeFrames.Count);
        }

        [Fact]
        public void GetFrames_InvalidFrame_ThrowsArgumentException()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 5, 6 };

            var exception = Assert.Throws<ArgumentException>(() => framingService.GetFrames(pinsDowned));
            Assert.Equal("Invalid frame(s): (5, 6)", exception.Message);
        }

        [Fact]
        public void GetFrames_MultipleInvalidFrames_ThrowsArgumentException()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 5, 6, 3, 9 };

            var exception = Assert.Throws<ArgumentException>(() => framingService.GetFrames(pinsDowned));
            Assert.Equal("Invalid frame(s): (5, 6), (3, 9)", exception.Message);
        }

        [Fact]
        public void GetFrames_InvalidNumberOfFramesAfterFinalStrike_ThrowsArgumentException()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 1 };

            var exception = Assert.Throws<ArgumentException>(() => framingService.GetFrames(pinsDowned));
            Assert.Equal("Invalid number of frames.", exception.Message);
        }

        [Fact]
        public void GetFrames_InvalidNumberOfFramesAfterFinalSpare_ThrowsArgumentException()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 9, 1, 2, 3 };

            var exception = Assert.Throws<ArgumentException>(() => framingService.GetFrames(pinsDowned));
            Assert.Equal("Invalid number of frames.", exception.Message);
        }

        [Fact]
        public void GetFrames_MoreFramesAfterGameCompletion_ThrowsArgumentException()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            var exception = Assert.Throws<ArgumentException>(() => framingService.GetFrames(pinsDowned));
            Assert.Equal("Invalid number of frames.", exception.Message);
        }

        [Fact]
        public void GetFrames_InvalidThrow_ThrowsArgumentException()
        {
            FramingService framingService = new FramingService();
            var pinsDowned = new List<int> { 10, 11 };

            var exception = Assert.Throws<ArgumentException>(() => framingService.GetFrames(pinsDowned));
            Assert.Equal("Invalid throw with 11 pins down.", exception.Message);
        }
    }
}
