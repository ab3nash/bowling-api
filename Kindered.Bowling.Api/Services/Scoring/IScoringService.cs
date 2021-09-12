using Kindred.Bowling.Api.Models;
using System.Collections.Generic;

namespace Kindred.Bowling.Api.Services.Scoring
{
    /// <summary>
    /// Defines all tasks related to scoring a 10 pin bowling game
    /// </summary>
    public interface IScoringService
    {
        /// <summary>
        /// Calculates the progress scores for a game according to the traditional scoring method
        /// If the progress score for a frame cannot be determined, it is marked as an asterisk(*)
        /// </summary>
        /// <param name="frames">All frames in a game</param>
        /// <returns>The list of Frame Progress Scores with a flag indicating whether or not the game is complete</returns>
        ScoringResultDto CalculateScore(List<Frame> frames);
    }
}
