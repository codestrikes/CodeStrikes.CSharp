using System;

namespace CodeCompetition.Sdk
{
    [Serializable]
    public class Move
    {
        public MoveType Type { get; }
        public Area Area { get; }

        public int GetAttackPower()
        {
            switch (this.Area)
            {
                case Area.Sensors: return 10;
                case Area.Head: return 5;
                case Area.Torso: return 1;
                case Area.Belly: return 1;
                default:
                    throw new ArgumentOutOfRangeException("Invalid area " + Area, (Exception)null);
            }
        }

        public int GetEnergy()
        {
            if (this.Type == MoveType.Attack)
            {
                switch (this.Area)
                {
                    case Area.Sensors: return 3;
                    case Area.Head: return 2;
                    case Area.Torso: return 1;
                    case Area.Belly: return 1;
                    default:
                        throw new ArgumentOutOfRangeException("Invalid area " + Area, (Exception)null);
                }
            }
            // Defense
            else
            {
                return 3;
            }
        }

        public Move(MoveType type, Area area)
        {
            Type = type;
            Area = area;
        }

        public override string ToString()
        {
            return $"{Type} on {Area} (P:{GetAttackPower()}, E:{GetEnergy()})";
        }
    }
}