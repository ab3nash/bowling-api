using Kindred.Bowling.Api.Models;
using System.Collections.Generic;

namespace Kindred.Bowling.Api.Services.Framing
{
    /// <summary>
    /// Handles tasks related to framing of a 10 Pin bowling game
    /// </summary>
    public interface IFramingService
    {
        /// <summary>
        /// Forms frames from a list of pins downed in game and validates the frames formed
        /// </summary>
        /// <exception cref="Exception">
        /// Can be thrown when pins downed is invalid to form Frames or formed Frame(s) are invalid
        /// </exception> 
        /// <param name="pinsDowned">All the pins downed in a game</param>
        /// <returns>List of frames corresponding to the pins downed</returns>
        List<Frame> GetFrames(List<int> pinsDowned);
    }
}
