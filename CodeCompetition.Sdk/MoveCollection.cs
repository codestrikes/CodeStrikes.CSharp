using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CodeStrikes.Sdk
{
    [Serializable]
    public class ReadonlyMoveCollection
    {
        protected readonly List<Move> moveList;

        public ReadOnlyCollection<Move> Moves => new ReadOnlyCollection<Move>(moveList);

        public ReadOnlyCollection<Move> GetAttacks()
        {
            return new ReadOnlyCollection<Move>(moveList.Where(x => x.Type == MoveType.Attack).ToList());
        }

        public ReadOnlyCollection<Move> GetDefences()
        {
            return new ReadOnlyCollection<Move>(moveList.Where(x => x.Type == MoveType.Defense).ToList());
        }

        public ReadonlyMoveCollection()
        {
            moveList = new List<Move>();
        }

        public override string ToString()
        {
            var res = moveList.Select(x => x.ToString()).Aggregate((x, y) => $"{x}, {y}");
            return res;
        }
    }

    [Serializable]
    public class MoveCollection : ReadonlyMoveCollection
    {
        public MoveCollection AddAttack(Area area)
        {
            Move move = new Move(MoveType.Attack, area);
            moveList.Add(move);
            return this;
        }

        public MoveCollection AddDefence(Area area)
        {
            Move move = new Move(MoveType.Defense, area);
            moveList.Add(move);
            return this;
        }

        public MoveCollection Remove(Move move)
        {
            moveList.Remove(move);
            return this;
        }
    }
}