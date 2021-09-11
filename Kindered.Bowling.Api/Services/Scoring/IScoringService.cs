using Kindered.Bowling.Api.Models;
using System.Collections.Generic;

namespace Kindered.Bowling.Api.Services.Scoring
{
    public interface IScoringService
    {
        ScoringResultDto CalculateScore(List<int> pinsDowned);
    }
}
