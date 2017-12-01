using System;
using System.Linq;

namespace CodeCompetition.Sdk.Bots
{
    public class Boxer : BotBase
    {
        private Area attack1 = Area.Sensors;
        private Area attack2 = Area.Head;
        private Area defence = Area.Sensors;

        private int myScoreTotal = 0;
        private int opponentScoreTotal = 0;

        private Area ChangeDefence(Area oldDefence)
        {
            return (oldDefence == Area.Sensors) ? Area.Head : Area.Sensors;
        }

        private Area CreateRandomAttack()
        {
            return new Random().NextDouble() > 0.5d ? Area.Belly : Area.Head;
        }

        public override MoveCollection NextMove(RoundContext context)
        {
            myScoreTotal += context.MyDamage;
            opponentScoreTotal += context.OpponentDamage;

            context.MyMoves
                .AddAttack(attack1)
                .AddAttack(attack2);

            if (context.LastOpponentMoves?.Attacks.Any(x => x.Area == defence) == false)
            {
                defence = ChangeDefence(defence);
            }

            if (myScoreTotal >= opponentScoreTotal)
                context.MyMoves.AddAttack(CreateRandomAttack()); // 3 attacks, 0 defence
            else
                context.MyMoves.AddDefence(defence);
            return context.MyMoves;
        }

        public override string ToString()
        {
            return "Boxer";
        }
    }
}
