using Kindred.Bowling.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kindred.Bowling.Api.Services.Framing
{
    /// <summary>
    /// Handles tasks related to framing of a 10 Pin bowling game
    /// </summary>
    public class FramingService : IFramingService
    {
        /// <summary>
        /// Forms frames from a list of pins downed in game and validates the frames formed
        /// </summary>
        /// <exception cref="Exception">
        /// Thrown when pins downed is invalid to form Frames or formed Frame(s) are invalid
        /// </exception> 
        /// <param name="pinsDowned">All the pins downed in a game</param>
        /// <returns>List of frames corresponding to the pins downed</returns>
        public List<Frame> GetFrames(List<int> pinsDowned)
        {
            var frames = new List<Frame>();
            if (pinsDowned == null || !pinsDowned.Any())
            {
                return frames;
            }

            int? lastThrowPinsDowned = null;
            for (int i = 0; i < pinsDowned.Count; i++)
            {
                if (pinsDowned[i] < 0 || pinsDowned[i] > 10)
                {
                    throw new Exception(string.Format("Invalid throw with {0} pins down.", pinsDowned[i]));
                }

                if (frames.Count > 10)
                {
                    throw new Exception("Invalid number of frames.");
                }

                if (frames.Count == 10)
                {
                    int remainingThrows = pinsDowned.Count - i;

                    if (frames.Last().IsStrike && remainingThrows <= 2)
                    {
                        if (remainingThrows == 2)
                            frames.Add(new Frame(pinsDowned[i], pinsDowned[i + 1], true));
                        else
                            frames.Add(new Frame(pinsDowned[i], null, true));
                    }
                    else if (frames.Last().IsSpare && remainingThrows == 1)
                    {
                        frames.Add(new Frame(pinsDowned[i], null, true));
                    }
                    else
                    {
                        throw new Exception("Invalid number of frames.");
                    }
                    break;
                }

                if (pinsDowned[i] == 10)
                {
                    if (lastThrowPinsDowned == null)
                    {
                        frames.Add(new Frame(10, null));
                    }
                    else if (lastThrowPinsDowned == 0)
                    {
                        frames.Add(new Frame(0, 10));
                    }
                    else
                    {
                        throw new Exception(string.Format("Invalid frame(s): ({0}, 10)", lastThrowPinsDowned));
                    }
                    lastThrowPinsDowned = null;
                    continue;
                }

                if (lastThrowPinsDowned == null)
                {
                    if (pinsDowned.Count - i == 1)
                    {
                        frames.Add(new Frame(pinsDowned[i], null));
                    }
                    lastThrowPinsDowned = pinsDowned[i];
                    continue;
                }

                frames.Add(new Frame(lastThrowPinsDowned.Value, pinsDowned[i]));
                lastThrowPinsDowned = null;
            }

            ValidateFrames(frames);

            return frames;
        }

        /// <summary>
        /// Validates all frames in a game, throws exception if invalid
        /// </summary>
        /// <exception cref="Exception">
        /// Thrown when one or more Frame(s) are invalid
        /// </exception> 
        /// <param name="frames"></param>
        private static void ValidateFrames(List<Frame> frames)
        {
            List<Frame> invalidFrames = frames.Where(f => !f.IsValid && !f.IsBonus && !f.IsIncomplete).ToList();
            if (invalidFrames.Any())
            {
                IEnumerable<string> invalidFrameString = invalidFrames.Select(f => string.Format(
                        "({0}, {1})", f.FirstThrowPinsDowned.ToString(), f.SecondThrowPinsDowned.ToString() ?? "null"));

                string exceptionMessage = "Invalid frame(s): ";
                exceptionMessage += string.Join(", ", invalidFrameString);


                throw new Exception(exceptionMessage);
            }
        }
    }
}
