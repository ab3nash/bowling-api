using System.Collections.Generic;

namespace Kindred.Bowling.Api.Models
{
    /// <summary>
    /// Models the scoring request parameters
    /// </summary>
    public class ScoringRequestDto
    {
        /// <summary>
        /// The list of pins downed on each throw
        /// </summary>
        public List<int> PinsDowned { get; set; }
    }
}
