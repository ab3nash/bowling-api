using Kindred.Bowling.Api.Models;
using System.Collections.Generic;

namespace Kindred.Bowling.Api.Services.Scoring
{
    public interface IScoringService
    {
        ScoringResultDto CalculateScore(List<Frame> frames);
    }
}
