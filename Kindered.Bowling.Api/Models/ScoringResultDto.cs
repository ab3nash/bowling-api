using System.Collections.Generic;

namespace Kindred.Bowling.Api.Models
{
    /// <summary>
    /// DTO to transfer scoring result of a 10 Pin Bowling game
    /// </summary>
    public class ScoringResultDto
    {
        /// <summary>
        /// The list of Frame Progress Scores for the game
        /// </summary>
        public List<string> FrameProgressScores { get; } = new List<string>();

        /// <summary>
        /// Indicates whether the game is complete
        /// </summary>
        public bool GameCompleted { get; set; }
    }
}
