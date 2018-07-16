using System;

namespace CodeStrikes.Sdk
{
    [Serializable]
    public class Move
    {
        public MoveType Type { get; }
        public Area Area { get; }

        public int GetAttackPower()
        {
            switch (Area)
            {
                case Area.HookKick: return 10;
                case Area.HookPunch: return 5;
                case Area.UppercutPunch: return 1;
                case Area.LowKick: return 1;
                default:
                    throw new ArgumentOutOfRangeException("Invalid area " + Area, (Exception)null);
            }
        }

        public int GetEnergy()
        {
            if (Type == MoveType.Attack)
            {
                switch (Area)
                {
                    case Area.HookKick: return 3;
                    case Area.HookPunch: return 2;
                    case Area.UppercutPunch: return 1;
                    case Area.LowKick: return 1;
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