using Kindred.Bowling.Api.Models;
using Kindred.Bowling.Api.Services.Framing;
using Kindred.Bowling.Api.Services.Scoring;
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
        private IFramingService _framingService;
        private IScoringService _scoringService;
        private readonly ILogger<ScoresController> _logger;

        public ScoresController(
            IFramingService framingService, IScoringService scoringService, ILogger<ScoresController> logger)
        {
            _framingService = framingService;
            _scoringService = scoringService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<ScoringResultDto> Post(ScoringRequestDto scoringRequest)
        {
            throw new NotImplementedException();
        }
    }
}
