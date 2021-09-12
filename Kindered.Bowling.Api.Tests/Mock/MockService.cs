using Kindred.Bowling.Api.Models;
using Kindred.Bowling.Api.Services.Framing;
using Kindred.Bowling.Api.Services.Scoring;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;

namespace Kindred.Bowling.Api.Tests.Mock
{
    public class MockService
    {
        #region IFramingService Mocks
        public static IFramingService GetFramingServiceMock()
        {
            // Mock the FramingService using Moq
            Mock<IFramingService> fakeFramingService = new Mock<IFramingService>();

            // Mock GetFrames to return list with a frame
            fakeFramingService.Setup(mr => mr.GetFrames(It.IsAny<List<int>>())).Returns(
                (List<int> pinsDowned) =>
                {
                    return new List<Frame> { new Frame(4, 5) };
                });

            return fakeFramingService.Object;
        }

        public static IFramingService GetFramingServiceMock_ThrowsArgumenException(string message)
        {
            // Mock the FramingService using Moq
            Mock<IFramingService> fakeFramingService = new Mock<IFramingService>();

            // Mock GetFrames to throw ArgumentException
            fakeFramingService.Setup(mr => mr.GetFrames(It.IsAny<List<int>>())).Returns(
                (List<int> pinsDowned) =>
                {
                    throw new ArgumentException(message);
                });

            return fakeFramingService.Object;
        }

        public static IFramingService GetFramingServiceMock_ThrowsException()
        {
            // Mock the FramingService using Moq
            Mock<IFramingService> fakeFramingService = new Mock<IFramingService>();

            // Mock GetFrames to throw Exception
            fakeFramingService.Setup(mr => mr.GetFrames(It.IsAny<List<int>>())).Returns(
                (List<int> pinsDowned) =>
                {
                    throw new Exception();
                });

            return fakeFramingService.Object;
        }
        #endregion

        #region IScoringService Mocks
        public static IScoringService GetScoringServiceMock()
        {
            // Mock the FramingService using Moq
            Mock<IScoringService> fakeScoringService = new Mock<IScoringService>();

            // Mock CalculateScore to return a ScoringResultDto
            fakeScoringService.Setup(mr => mr.CalculateScore(It.IsAny<List<Frame>>())).Returns(
                (List<Frame> frames) =>
                {
                    var result = new ScoringResultDto();
                    result.FrameProgressScores.Add("10");
                    result.FrameProgressScores.Add("*");
                    result.GameCompleted = false;
                    return result;
                });

            return fakeScoringService.Object;
        }

        public static IScoringService GetScoringServiceMock_ThrowsException()
        {
            // Mock the FramingService using Moq
            Mock<IScoringService> fakeScoringService = new Mock<IScoringService>();

            // Mock CalculateScore to throw Exception
            fakeScoringService.Setup(mr => mr.CalculateScore(It.IsAny<List<Frame>>())).Returns(
                (List<Frame> frames) =>
                {
                    throw new Exception();
                });

            return fakeScoringService.Object;
        }
        #endregion

        #region Logger Mocks
        public static ILogger<T> GetLoggerMock<T>()
        {
            // Return NullLogger to replace logger
            // Substitute for Mock Logger, NullLogger does nothing
            return new NullLogger<T>();
        }
        #endregion
    }
}
