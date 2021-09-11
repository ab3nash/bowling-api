using Kindred.Bowling.Api.Models;
using Xunit;

namespace Kindred.Bowling.Api.Tests.ModelTests
{
    public class FrameTests
    {
        [Fact]
        public void Constructor_2Params_ObjectIsInitialized()
        {
            Frame frame = new Frame(1, 2);
            Assert.NotNull(frame);
        }

        [Fact]
        public void Constructor_2Params_FirstThrowPinsDownedIsSet()
        {
            Frame frame = new Frame(1, 2);
            Assert.Equal(1, frame.FirstThrowPinsDowned);
        }

        [Fact]
        public void Constructor_2Params_SecondThrowPinsDownedIsSet()
        {
            Frame frame = new Frame(1, 2);
            Assert.Equal(2, frame.SecondThrowPinsDowned);
        }

        [Fact]
        public void Constructor_3Params_ObjectIsInitialized()
        {
            Frame frame = new Frame(1, 2, false);
            Assert.NotNull(frame);
        }

        [Fact]
        public void Constructor_3Params_FirstThrowPinsDownedIsSet()
        {
            Frame frame = new Frame(1, 2, false);
            Assert.Equal(1, frame.FirstThrowPinsDowned);
        }

        [Fact]
        public void Constructor_3Params_SecondThrowPinsDownedIsSet()
        {
            Frame frame = new Frame(1, 2, false);
            Assert.Equal(2, frame.SecondThrowPinsDowned);
        }

        [Theory]
        [InlineData(1, 2, false, false)]
        [InlineData(1, 2, true, true)]
        public void Constructor_3Params_IsBonusIsSet(
            int firstThrowPinsDowned,
            int secondThrowPinsDowned,
            bool setIsBonus,
            bool expectedIsBonus)
        {
            Frame frame = new Frame(firstThrowPinsDowned, secondThrowPinsDowned, setIsBonus);
            Assert.Equal(expectedIsBonus, frame.IsBonus);
        }

        [Theory]
        [InlineData(1, -1, false)]
        [InlineData(-1, 1, false)]
        [InlineData(-1, -1, false)]
        [InlineData(1, 10, false)]
        [InlineData(10, 1, false)]
        [InlineData(1, 0, true)]
        [InlineData(0, 1, true)]
        [InlineData(1, 9, true)]
        [InlineData(9, 1, true)]
        [InlineData(2, 5, true)]
        [InlineData(2, null, false)]
        [InlineData(11, null, false)]
        [InlineData(10, null, true)]
        public void IsValid_ReturnsCorrectResult(int firstThrowPinsDowned, int? secondThrowPinsDowned, bool expectedValidity)
        {
            Frame frame = new Frame(firstThrowPinsDowned, secondThrowPinsDowned);
            Assert.Equal(expectedValidity, frame.IsValid);
        }

        [Theory]
        [InlineData(1, 0, false)]
        [InlineData(0, 1, false)]
        [InlineData(10, null, true)]
        [InlineData(0, 10, true)]
        [InlineData(2, 8, false)]
        [InlineData(9, 1, false)]
        public void IsStrike_ReturnsCorrectResult(int firstThrowPinsDowned, int? secondThrowPinsDowned, bool expectedStrike)
        {
            Frame frame = new Frame(firstThrowPinsDowned, secondThrowPinsDowned);
            Assert.Equal(expectedStrike, frame.IsStrike);
        }

        [Theory]
        [InlineData(1, 0, false)]
        [InlineData(0, 1, false)]
        [InlineData(10, 0, false)]
        [InlineData(0, 10, false)]
        [InlineData(2, 8, true)]
        [InlineData(9, 1, true)]
        public void IsSpare_ReturnsCorrectResult(int firstThrowPinsDowned, int? secondThrowPinsDowned, bool expectedSpare)
        {
            Frame frame = new Frame(firstThrowPinsDowned, secondThrowPinsDowned);
            Assert.Equal(expectedSpare, frame.IsSpare);
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(0, 1, true)]
        [InlineData(10, 0, false)]
        [InlineData(0, 10, false)]
        [InlineData(2, 8, false)]
        [InlineData(9, 1, false)]
        [InlineData(8, 1, true)]
        public void IsOpen_ReturnsCorrectResult(int firstThrowPinsDowned, int? secondThrowPinsDowned, bool expectedOpen)
        {
            Frame frame = new Frame(firstThrowPinsDowned, secondThrowPinsDowned);
            Assert.Equal(expectedOpen, frame.IsOpen);
        }

        [Theory]
        [InlineData(0, null, true)]
        [InlineData(10, null, false)]
        [InlineData(9, 0, false)]
        [InlineData(0, 1, false)]
        [InlineData(2, null, true)]
        [InlineData(9, null, true)]
        public void IsIncomplete_ReturnsCorrectResult(int firstThrowPinsDowned, int? secondThrowPinsDowned, bool expectedOpen)
        {
            Frame frame = new Frame(firstThrowPinsDowned, secondThrowPinsDowned);
            Assert.Equal(expectedOpen, frame.IsIncomplete);
        }
    }
}
