using Kindred.Bowling.Api.Models;
using Kindred.Bowling.Api.Services.Framing;
using Kindred.Bowling.Api.Services.Scoring;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Kindred.Bowling.Api.Controllers
{
    /// <summary>
    /// Handles requests related to 10 Pin bowling scores
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly IFramingService _framingService;
        private readonly IScoringService _scoringService;
        private readonly ILogger<ScoresController> _logger;

        /// <summary>
        /// Creates an instance of ScoresController class
        /// </summary>
        /// <param name="framingService">An implementation of IFramingService</param>
        /// <param name="scoringService">An implementation of IScoringService</param>
        /// <param name="logger">The logger</param>
        public ScoresController(
            IFramingService framingService, IScoringService scoringService, ILogger<ScoresController> logger)
        {
            _framingService = framingService;
            _scoringService = scoringService;
            _logger = logger;
        }

        /// <summary>
        /// Calculates the scores and checks if game is completed for a set of pins downed per throw.
        /// Gets Frames from the pinsDowned list and calculates frame progress score per frame.
        /// </summary>
        /// <param name="scoringRequest">Scoring request object with list of pins downed per throw</param>
        /// <returns>
        /// Either One of:
        /// 1. OK with ScoringResultDto for success
        /// 2. Badrequest with ModelState dictionary for bad requests
        /// 3. InternalServerError
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(ScoringResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ScoringResultDto> Post(ScoringRequestDto scoringRequest)
        {
            if (scoringRequest == null)
            {
                ModelState.AddModelError(nameof(scoringRequest.PinsDowned), "Scoring request object is empty.");
                return BadRequest(ModelState);
            }
            if (scoringRequest.PinsDowned == null)
            {
                ModelState.AddModelError(nameof(scoringRequest.PinsDowned), "PinsDowned is required.");
                return BadRequest(ModelState);
            }

            List<Frame> frames;
            try
            {
                frames = _framingService.GetFrames(scoringRequest.PinsDowned);
            }
            catch (ArgumentException a)
            {
                _logger.LogError(a, a.Message);

                ModelState.AddModelError(nameof(scoringRequest.PinsDowned), a.Message);
                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            ScoringResultDto scoringResult;
            try
            {
                scoringResult = _scoringService.CalculateScore(frames);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(scoringResult);

        }
    }
}
