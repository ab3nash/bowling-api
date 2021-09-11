namespace Kindred.Bowling.Api.Models
{
    public class Frame
    {
        public Frame(int firstThrowPinsDowned, int? secondThrowPinsDowned)
        {
            FirstThrowPinsDowned = firstThrowPinsDowned;
            SecondThrowPinsDowned = secondThrowPinsDowned;
        }

        public Frame(int firstThrowPinsDowned, int? secondThrowPinsDowned, bool isBonus)
        {
            FirstThrowPinsDowned = firstThrowPinsDowned;
            SecondThrowPinsDowned = secondThrowPinsDowned;
            IsBonus = isBonus;
        }

        public int FirstThrowPinsDowned { get; private set; }
        public int? SecondThrowPinsDowned { get; private set; }
        public bool IsBonus { get; private set; }

        public bool IsIncomplete
        {
            get
            {
                return FirstThrowPinsDowned >= 0 &&
                    FirstThrowPinsDowned < 10 &&
                    !SecondThrowPinsDowned.HasValue;
            }
        }

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

        public bool IsOpen
        {
            get
            {
                return IsValid && !IsIncomplete && !IsStrike && !IsSpare;
            }
        }
    }
}
