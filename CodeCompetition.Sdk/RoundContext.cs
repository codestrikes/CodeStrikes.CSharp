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

        public RoundContext(ReadonlyMoveCollection lastOpponentMoves, int myDamage, int opponentDamage)
        {
            LastOpponentMoves = lastOpponentMoves;
            MyDamage = myDamage;
            OpponentDamage = opponentDamage;
            MyMoves = new MoveCollection();
        }

        public void SetMoves(MoveCollection moves)
        {
            MyMoves = moves;
        }
    }
}
