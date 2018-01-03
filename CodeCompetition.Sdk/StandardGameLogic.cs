using System;
using System.Linq;

namespace CodeCompetition.Sdk
{
    public interface IGameLogic
    {
        int LifePoints { get; }
        TimeSpan MaxMoveTime { get; }
        bool Validate(ReadonlyMoveCollection moveCollection);
        bool ValidateRound(int roundNumber, int f1Points, int f2Points);
        int CalculateScore(ReadonlyMoveCollection attacker, ReadonlyMoveCollection defender);
    }

    public class StandardGameLogic : IGameLogic
    {
        public int LifePoints => 200;        
        public int Energy => 9;
        public int MaxRounds => 200;

        public TimeSpan MaxMoveTime { get; }

        public StandardGameLogic()
        {
            MaxMoveTime = TimeSpan.FromSeconds(0.5);
        }

        public int CalculateScore(ReadonlyMoveCollection attacker, ReadonlyMoveCollection defender)
        {
            int points = 0;

            foreach (var attack in attacker.Moves.Where(x => x.Type == MoveType.Attack))
            {
                if (!defender.Moves.Any(x => x.Type == MoveType.Defense && x.Area == attack.Area))
                {
                    points += attack.GetAttackPower();
                }                    
            }

            return points;
        }

        public bool Validate(ReadonlyMoveCollection moveCollection)
        {
            return moveCollection?.Moves.Sum(x => x.GetEnergy()) <= Energy;
        }

        public bool ValidateRound(int roundNumber, int f1Points, int f2Points)
        {
            if (roundNumber >= MaxRounds && f1Points > 0 && f2Points > 0) return false;
            if (f1Points < 0 && f2Points < 0) return false;
            return true;
        }
    }
}
