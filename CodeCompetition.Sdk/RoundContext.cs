using System;

namespace CodeStrikes.Sdk
{
    [Serializable]
    public struct RoundContext
    {
        public MoveCollection MyMoves { get; private set; }
        public ReadonlyMoveCollection LastOpponentMoves { get; }
        public int MyDamage { get; }
        public int OpponentDamage { get; }
        public int MyLifePoints { get; }
        public int OpponentLifePoints { get; set; }

        public RoundContext(ReadonlyMoveCollection lastOpponentMoves, int myDamage, int opponentDamage, int myLifePoints, int opponentLifePoints)
        {
            LastOpponentMoves = lastOpponentMoves;
            MyDamage = myDamage;
            OpponentDamage = opponentDamage;
            MyLifePoints = myLifePoints;
            OpponentLifePoints = opponentLifePoints;

            MyMoves = new MoveCollection();
        }

        public void SetMoves(MoveCollection moves)
        {
            MyMoves = moves;
        }
    }
}
