using Kindred.Bowling.Api.Models;
using System;
using System.Collections.Generic;

namespace Kindred.Bowling.Api.Services.Scoring
{
    public class ScoringService : IScoringService
    {
        public ScoringResultDto CalculateScore(List<int> pinsDowned)
        {
            var scoringResult = new ScoringResultDto();

            if(pinsDowned == null || pinsDowned.Count < 2)
            {
                return scoringResult;
            }

            throw new NotImplementedException();
        }
    }
}
