using System.Collections.Generic;

namespace Kindred.Bowling.Api.Models
{
    public class ScoringResultDto
    {
        public List<string> FrameProgressScores { get; private set; } = new List<string>();
        public bool GameCompleted { get; set; }
    }
}
