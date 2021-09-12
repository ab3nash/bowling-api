using Kindred.Bowling.Api.Controllers;
using Kindred.Bowling.Api.Models;
using Kindred.Bowling.Api.Tests.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace Kindred.Bowling.Api.Tests.ControllerTests
{
    public class ScoresControllerTests
    {
        #region Scores/Post
        #region Success
        [Fact]
        public void Post_ValidPinsDowned_ResponseNotNull()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 2, 4 }
            };

            var response = scoresController.Post(scoringRequest);

            Assert.NotNull(response);
        }

        [Fact]
        public void Post_ValidPinsDowned_ResponseHasCorrectType()
        {
            var scoresController = new ScoresController(
                 MockService.GetFramingServiceMock(),
                 MockService.GetScoringServiceMock(),
                 MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 2, 4 }
            };

            var response = scoresController.Post(scoringRequest);

            Assert.IsType<ActionResult<ScoringResultDto>>(response);
        }

        [Fact]
        public void Post_ValidPinsDowned_ResultIsOKObjectResult()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 2, 4 }
            };

            var response = scoresController.Post(scoringRequest);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(response.Result);

            Assert.IsAssignableFrom<ScoringResultDto>(okResult.Value as ScoringResultDto);
        }
        #endregion

        #region Bad Request - NullPinsDowned
        [Fact]
        public void Post_NullPinsDowned_ResponseNotNull()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto();

            var response = scoresController.Post(scoringRequest);

            Assert.NotNull(response);
        }

        [Fact]
        public void Post_NullPinsDowned_ResultIsBadRequestObjectResult()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto();

            var response = scoresController.Post(scoringRequest);
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void Post_NullPinsDowned_ResultHasCorrectErrorMessage()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto();

            var response = scoresController.Post(scoringRequest);
            BadRequestObjectResult badRequestResult = response.Result as BadRequestObjectResult;
            SerializableError error = badRequestResult.Value as SerializableError;
            var errorMessage = (error["pinsDowned"] as string[])[0];

            Assert.Equal("PinsDowned is required.", errorMessage);
        }
        #endregion

        #region Bad Request - InvalidPinsDowned
        [Fact]
        public void Post_InvalidPinsDowned_ResponseNotNull()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            scoresController.ModelState.AddModelError("pinsDowned", "Invalid throw with 11 pins down.");
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 11, 21, -2 }
            };

            var response = scoresController.Post(scoringRequest);

            Assert.NotNull(response);
        }

        [Fact]
        public void Post_InvalidPinsDowned_ResultIsBadRequestObjectResult()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            scoresController.ModelState.AddModelError("pinsDowned", "Invalid throw with 11 pins down.");
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 11, 21, -2 }
            };

            var response = scoresController.Post(scoringRequest);

            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void Post_InvalidPinsDowned_ResultHasCorrectErrorMessage()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            scoresController.ModelState.AddModelError("pinsDowned", "Invalid throw with 11 pins down.");
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 11, 21, -2 }
            };

            var response = scoresController.Post(scoringRequest);

            BadRequestObjectResult badRequestResult = response.Result as BadRequestObjectResult;
            SerializableError error = badRequestResult.Value as SerializableError;
            var errorMessage = (error["pinsDowned"] as string[])[0];

            Assert.Equal("Invalid throw with 11 pins down.", errorMessage);
        }
        #endregion

        #region Bad Request - IFramingService throws ArgumentException
        [Fact]
        public void Post_FramingServiceThrowsArgumentException_ResponseNotNull()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock_ThrowsArgumenException("Test Exception Message"),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 1, 2 }
            };

            var response = scoresController.Post(scoringRequest);

            Assert.NotNull(response);
        }

        [Fact]
        public void Post_FramingServiceThrowsArgumentException_ResultIsBadRequestObjectResult()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock_ThrowsArgumenException("Test Exception Message"),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 1, 2 }
            };

            var response = scoresController.Post(scoringRequest);

            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void Post_FramingServiceThrowsArgumentException_ResultHasCorrectErrorMessage()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock_ThrowsArgumenException("Test Exception Message"),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 1, 2 }
            };

            var response = scoresController.Post(scoringRequest);

            BadRequestObjectResult badRequestResult = response.Result as BadRequestObjectResult;
            SerializableError error = badRequestResult.Value as SerializableError;
            var errorMessage = (error["pinsDowned"] as string[])[0];

            Assert.Equal("Test Exception Message", errorMessage);
        }
        #endregion

        #region InternalServerError
        [Fact]
        public void Post_FramingServiceThrowsException_ResponseNotNull()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock_ThrowsException(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 1, 2 }
            };

            var response = scoresController.Post(scoringRequest);

            Assert.NotNull(response);
        }

        [Fact]
        public void Post_FramingServiceThrowsException_ResultIsBadRequestObjectResult()
        {
            var scoresController = new ScoresController(
                MockService.GetFramingServiceMock_ThrowsException(),
                MockService.GetScoringServiceMock(),
                MockService.GetLoggerMock<ScoresController>());
            var scoringRequest = new ScoringRequestDto
            {
                PinsDowned = new List<int> { 1, 2 }
            };

            var response = scoresController.Post(scoringRequest);

            StatusCodeResult statusCodeResult = Assert.IsType<StatusCodeResult>(response.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
        #endregion
        #endregion
    }
}
