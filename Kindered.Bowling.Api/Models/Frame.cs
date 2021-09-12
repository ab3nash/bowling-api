namespace Kindred.Bowling.Api.Models
{
    /// <summary>
    /// Represents a frame in a 10 pin bowlign game
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// Initialises a new instance of Frame class
        /// </summary>
        /// <param name="firstThrowPinsDowned">The number of pins downed in the first throw of the frame</param>
        /// <param name="secondThrowPinsDowned">The number of pins downed in the second throw of the frame</param>
        public Frame(int firstThrowPinsDowned, int? secondThrowPinsDowned)
        {
            FirstThrowPinsDowned = firstThrowPinsDowned;
            SecondThrowPinsDowned = secondThrowPinsDowned;
        }

        /// <summary>
        /// Initialises a new instance of Frame class
        /// </summary>
        /// <param name="firstThrowPinsDowned">The number of pins downed in the first throw of the frame</param>
        /// <param name="secondThrowPinsDowned">The number of pins downed in the second throw of the frame</param>
        /// <param name="isBonus">Whether the frame is a bonus frame after a final throw of strike or spare</param>
        public Frame(int firstThrowPinsDowned, int? secondThrowPinsDowned, bool isBonus)
        {
            FirstThrowPinsDowned = firstThrowPinsDowned;
            SecondThrowPinsDowned = secondThrowPinsDowned;
            IsBonus = isBonus;
        }

        /// <summary>
        /// The number of pins downed in the first throw of the frame
        /// </summary>
        public int FirstThrowPinsDowned { get; private set; }

        /// <summary>
        /// The number of pins downed in the second throw of the frame
        /// </summary>
        public int? SecondThrowPinsDowned { get; private set; }

        /// <summary>
        /// A flag indicating if the frame is a bonus frame after a final throw of strike or spare 
        /// </summary>
        public bool IsBonus { get; private set; }

        /// <summary>
        /// A flag indicating if the frame is incomplete
        /// </summary>
        public bool IsIncomplete
        {
            get
            {
                return FirstThrowPinsDowned >= 0 &&
                    FirstThrowPinsDowned < 10 &&
                    !SecondThrowPinsDowned.HasValue;
            }
        }

        /// <summary>
        /// A flag indicating if the frame is valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return (FirstThrowPinsDowned >= 0 &&
                    ((FirstThrowPinsDowned != 10 &&
                    SecondThrowPinsDowned.HasValue &&
                    SecondThrowPinsDowned.Value >= 0 &&
                    FirstThrowPinsDowned + SecondThrowPinsDowned.Value <= 10) ||
                    (FirstThrowPinsDowned == 10 && !SecondThrowPinsDowned.HasValue)));
            }
        }

        /// <summary>
        /// A flag indicating if the frame is a strike frame
        /// </summary>
        public bool IsStrike
        {
            get
            {
                return IsValid &&
                    !IsIncomplete &&
                    ((FirstThrowPinsDowned == 10 && !SecondThrowPinsDowned.HasValue) ||
                    (FirstThrowPinsDowned == 0 && SecondThrowPinsDowned.HasValue && SecondThrowPinsDowned == 10));
            }
        }

        /// <summary>
        /// A flag indicating if the frame is a spare frame
        /// </summary>
        public bool IsSpare
        {
            get
            {
                return IsValid &&
                    !IsIncomplete &&
                    !IsStrike &&
                    (FirstThrowPinsDowned + SecondThrowPinsDowned.Value == 10);
            }
        }

        /// <summary>
        /// A flag indicating if the frame is an open frame
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return IsValid && !IsIncomplete && !IsStrike && !IsSpare;
            }
        }
    }
}
