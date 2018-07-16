using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeStrikes.Sdk;

namespace CodeStrikes.TestingApp
{
    class PlayerBot : BotBase
    {
        int round = 0;
        Random rand = new Random();
        private int myScore = 0;
        private int hisScore = 0;
        private double wageModifer = 0;
        private MovesCollection collection = new MovesCollection();
        private MoveCollection lastMove;
        private int timesWithoutNoseDefense = 0;
        private int timesWithoutJawDefense = 0;

        public override MoveCollection NextMove(RoundContext context)
        {
            if (lastMove != null)
            {
                collection.AddMove(lastMove, context.MyDamage - context.OpponentDamage);
            }

            MoveCollection move = new MoveCollection();

            //First move
            if (context.LastOpponentMoves == null)
            {
                move.AddAttack(Area.LowKick)
                    .AddDefence(Area.HookPunch)
                    .AddDefence(Area.HookKick);
            }
            else
            {
                var superAttackRand = rand.NextDouble();
                collection.MakeBestMove(move);
            }

            if (context.LastOpponentMoves != null)
            {
                collection.AddMove(context.LastOpponentMoves, context.OpponentDamage - context.MyDamage, true);
            }

            round++;
            myScore += context.MyDamage;
            hisScore += context.OpponentDamage;
            lastMove = move;

            var defenses = move.GetDefences();
            if (defenses.Count() < defenses.Count)
            {
                move.Remove(defenses[0]);
                MakeRandomAttack(move);
            }
            if (context.LastOpponentMoves != null)
            {
                if (!context.LastOpponentMoves.GetDefences().Contains(Area.HookKick))
                {
                    timesWithoutJawDefense++;
                    if (timesWithoutJawDefense > 1)
                    {
                        move.Clear();
                        move.AddAttack(Area.HookKick).AddAttack(Area.HookKick).AddDefence(collection.GetBestDeffense());
                        timesWithoutJawDefense = 0;
                    }
                }

                if (!context.LastOpponentMoves.GetDefences().Contains(Area.HookPunch))
                {
                    timesWithoutNoseDefense++;
                    if (timesWithoutNoseDefense > 1)
                    {
                        move.Clear();
                        move.AddAttack(Area.HookPunch).AddAttack(Area.HookPunch).AddDefence(collection.GetBestDeffense());
                        timesWithoutNoseDefense = 0;
                    }
                }
            }


            FixMoves(move);

            return move;
        }

        public void FixMoves(MoveCollection move)
        {
            while (move.Moves.Sum(x => x.GetEnergy()) > 9)
            {
                move.Remove(move.Moves[0]);
            }
            while (move.Moves.Sum(x => x.GetEnergy()) < 9)
            {
                move.AddAttack(Area.UppercutPunch);
            }
        }

        public MoveCollection MakeRandomAttack(MoveCollection move)
        {
            return move.AddAttack(MakeRandomArea());
        }

        public Area MakeRandomArea()
        {
            double random = rand.NextDouble();
            if (random < 0.25 + wageModifer)
                return Area.HookKick;

            if (random < 0.6 + wageModifer)
                return Area.HookPunch;

            if (random < 0.8 + wageModifer)
                return Area.LowKick;

            if (random < 0.95 + wageModifer)
                return Area.LowKick;
            return Area.UppercutPunch;
        }

        enum MoveType
        {
            Attack,
            Defense
        }

        class MovesWithScore : IComparable<MovesWithScore>
        {
            public List<MoveWithPoints> Moves { get; set; }

            public int Score { get; set; }

            public bool OpponentMove { get; set; }

            public MovesWithScore()
            {
                Moves = new List<MoveWithPoints>();
            }

            public void SetDefense(Area area)
            {
                var def = Moves.Where(x => x.Type == MoveType.Defense && x.Area != area).FirstOrDefault();
                if (def != null)
                {
                    def.Area = area;
                }

            }

            public void FromMove(ReadonlyMoveCollection move)
            {
                int i = 0;
                foreach (var attack in move.GetAttacks())
                {
                    Moves.Add(new MoveWithPoints(MoveType.Attack, attack.Area));
                    i++;
                }
                foreach (var def in move.GetDefences())
                {
                    Moves.Add(new MoveWithPoints(MoveType.Defense, def.Area));
                    i++;
                }
            }


            public override int GetHashCode()
            {
                var sum = Moves.Sum(x => x.Points);
                return sum;
            }

            internal void Apply(MoveCollection move)
            {
                foreach (var mvs in Moves)
                {
                    if (mvs.Type == MoveType.Attack)
                    {
                        move.AddAttack(mvs.Area);
                    }
                    else
                    {
                        move.AddDefence(mvs.Area);
                    }
                }
            }

            public int CompareTo(MovesWithScore other)
            {
                return other.Score.CompareTo(Score);
            }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("S:").Append(Score);
                foreach (var move in Moves)
                {
                    builder.Append(move);
                }
                return builder.ToString();
            }
        }

        class MoveWithPoints
        {
            public MoveType Type { get; set; }
            public Area Area { get; set; }

            public int Points
            {
                get
                {
                    if (Type == MoveType.Attack)
                        return (int)Area;
                    else
                        return -1 * (int)Area;
                }
            }

            public MoveWithPoints(MoveType type, Area area)
            {
                Type = type;
                Area = area;
            }

            public override int GetHashCode()
            {
                return Points.GetHashCode();
            }

            public override string ToString()
            {
                if (Type == MoveType.Attack)
                {
                    return "A:" + (int)Area;
                }
                else
                {
                    return "D:" + (int)Area;
                }
            }
        }

        class MovesCollection
        {
            SortedSet<MovesWithScore> BestMoves { get; set; }
            SortedSet<MovesWithScore> OpponentMoves { get; set; }
            Random rand = new Random();

            public MovesCollection()
            {
                BestMoves = new SortedSet<MovesWithScore>();
                OpponentMoves = new SortedSet<MovesWithScore>();

                BestMoves.Add(new MovesWithScore { Moves = new List<MoveWithPoints> { new MoveWithPoints(MoveType.Attack, Area.HookKick), new MoveWithPoints(MoveType.Attack, Area.HookKick), new MoveWithPoints(MoveType.Defense, Area.HookKick) }, Score = 10 });
                BestMoves.Add(new MovesWithScore { Moves = new List<MoveWithPoints> { new MoveWithPoints(MoveType.Attack, Area.HookPunch), new MoveWithPoints(MoveType.Attack, Area.HookPunch), new MoveWithPoints(MoveType.Defense, Area.HookKick) }, Score = 9 });
                BestMoves.Add(new MovesWithScore { Moves = new List<MoveWithPoints> { new MoveWithPoints(MoveType.Attack, Area.LowKick), new MoveWithPoints(MoveType.Attack, Area.LowKick), new MoveWithPoints(MoveType.Defense, Area.HookKick) }, Score = 8 });
                BestMoves.Add(new MovesWithScore { Moves = new List<MoveWithPoints> { new MoveWithPoints(MoveType.Attack, Area.LowKick), new MoveWithPoints(MoveType.Attack, Area.UppercutPunch), new MoveWithPoints(MoveType.Defense, Area.HookKick) }, Score = 7 });
                BestMoves.Add(new MovesWithScore { Moves = new List<MoveWithPoints> { new MoveWithPoints(MoveType.Attack, Area.LowKick), new MoveWithPoints(MoveType.Defense, Area.HookPunch), new MoveWithPoints(MoveType.Defense, Area.HookKick) }, Score = 6 });
                BestMoves.Add(new MovesWithScore { Moves = new List<MoveWithPoints> { new MoveWithPoints(MoveType.Attack, Area.HookKick), new MoveWithPoints(MoveType.Defense, Area.HookPunch), new MoveWithPoints(MoveType.Defense, Area.HookKick) }, Score = 5 });
                BestMoves.Add(new MovesWithScore { Moves = new List<MoveWithPoints> { new MoveWithPoints(MoveType.Attack, Area.HookPunch), new MoveWithPoints(MoveType.Defense, Area.HookPunch), new MoveWithPoints(MoveType.Defense, Area.HookKick) }, Score = 4 });
            }

            public void AddMove(ReadonlyMoveCollection move, int score, bool opponentMove = false)
            {
                MovesWithScore mvs = new MovesWithScore();
                mvs.Score = score;
                mvs.FromMove(move);
                mvs.OpponentMove = opponentMove;

                var existingMove = BestMoves.FirstOrDefault(m => m.GetHashCode() == mvs.GetHashCode());
                if (existingMove == null)
                {
                    BestMoves.Add(mvs);
                }
                else
                {
                    BestMoves.Remove(existingMove);
                    BestMoves.Add(mvs);
                }
                if (opponentMove)
                {
                    OpponentMoves.Add(mvs);
                }
            }

            public Area GetBestDeffense()
            {
                var defenses = OpponentMoves
                    .SelectMany(x => x.Moves.Select(z => new { z.Area, Count = (int)z.Area }));
                if (!defenses.Any())
                {
                    return Area.HookKick;
                }

                var groups = defenses
                    .GroupBy(x => x.Area)
                    .Select(x => new { x.Key, Count = x.Sum(z => z.Count) })
                    .OrderByDescending(x => x.Count);
                var res = groups
                    .Select(x => x.Key)
                    .FirstOrDefault();
                return res;
            }

            public void MakeBestMove(MoveCollection move)
            {
                var randomizer = rand.NextDouble();
                int skip = 0;
                if (randomizer <= 0.5) skip = 0;
                else skip = 1;

                if (BestMoves.Count < skip + 1)
                    skip = 0;

                var bm = BestMoves.Skip(skip).First();

                try
                {
                    bm.SetDefense(GetBestDeffense());
                }
                catch (Exception ex)
                {
                    //            ex.ToString();
                }
                bm.Apply(move);
            }
        }
    }

    public static class MoveCollectionExtensions
    {
        public static void Clear(this MoveCollection collection)
        {
            foreach (var move in collection.Moves.ToList())
            {
                collection.Remove(move);
            }
        }

        public static bool Contains(this IReadOnlyCollection<Move> collection, Area area)
        {
            return collection.Any(x => x.Area == area);
        }
    }
}