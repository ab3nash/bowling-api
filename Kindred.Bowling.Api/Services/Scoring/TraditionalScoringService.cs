using Kindred.Bowling.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Kindred.Bowling.Api.Services.Scoring
{
    /// <summary>
    /// Handles all tasks related to scoring a 10 pin bowling game
    /// </summary>
    public class TraditionalScoringService : IScoringService
    {
        /// <summary>
        /// Calculates the progress scores for a game according to the traditional scoring method
        /// If the progress score for a frame cannot be determined, it is marked as an asterisk(*)
        /// More information on the scoring process may be found in the following links:
        /// https://en.wikipedia.org/wiki/Ten-pin_bowling#Traditional_scoring
        /// https://www.bowlinggenius.com/
        /// https://www.liveabout.com/bowling-scoring-420895
        /// </summary>
        /// <param name="frames">All frames in a game</param>
        /// <returns>The list of Frame Progress Scores with a flag indicating whether or not the game is complete</returns>
        public ScoringResultDto CalculateScore(List<Frame> frames)
        {
            var scoringResult = new ScoringResultDto();

            if (frames == null || !frames.Any())
            {
                return scoringResult;
            }

            int totalScore = 0;
            for (int i = 0; i < frames.Count; i++)
            {
                Frame frame = frames[i];

                int? frameProgressScore = null;

                if (frame.IsBonus)
                {
                    continue;
                }
                else if (frame.IsOpen)
                {
                    frameProgressScore = frame.FirstThrowPinsDowned + frame.SecondThrowPinsDowned.Value;
                }
                else if (frame.IsSpare && frames.Count - i > 1)
                {
                    Frame nextFrame = frames[i + 1];

                    frameProgressScore = frame.FirstThrowPinsDowned + frame.SecondThrowPinsDowned.Value +
                        nextFrame.FirstThrowPinsDowned;
                }
                else if (frame.IsStrike && frames.Count - i > 1)
                {

                    Frame nextFrame = frames[i + 1];

                    if (nextFrame.FirstThrowPinsDowned == 10 && !nextFrame.IsBonus && frames.Count - i > 2)
                    {
                        Frame thridFrame = frames[i + 2];
                        frameProgressScore = frame.FirstThrowPinsDowned + (frame.SecondThrowPinsDowned ?? 0) +
                            10 + thridFrame.FirstThrowPinsDowned;
                    }
                    else if (!nextFrame.IsIncomplete)
                    {
                        frameProgressScore = frame.FirstThrowPinsDowned + (frame.SecondThrowPinsDowned ?? 0) +
                            nextFrame.FirstThrowPinsDowned + (nextFrame.SecondThrowPinsDowned ?? 0);
                    }
                }

                totalScore += frameProgressScore ?? 0;
                scoringResult.FrameProgressScores.Add(frameProgressScore == null ? "*" : totalScore.ToString());
            }

            scoringResult.GameCompleted = IsGameComplete(frames);
            return scoringResult;
        }

        /// <summary>
        /// Checks if a game is complete
        /// </summary>
        /// <param name="frames">All frames in a game</param>
        /// <returns>True if complete, False otherwise</returns>
        private bool IsGameComplete(List<Frame> frames)
        {
            if (frames.Count == 10)
            {
                Frame lastFrame = frames.Last();
                if (lastFrame.IsSpare || lastFrame.IsStrike)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            if (frames.Count == 11)
            {
                Frame lastFrame = frames[9];
                Frame currentFrame = frames[10];

                if (lastFrame.IsStrike && (!currentFrame.IsBonus || !currentFrame.SecondThrowPinsDowned.HasValue))
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
