using System;
using System.Linq;

namespace CodeStrikes.Sdk.Bots
{
    public class Boxer : BotBase
    {
        private readonly Area attack1 = Area.HookKick;
        private readonly Area attack2 = Area.HookPunch;
        private Area defence = Area.HookKick;

        private int myScoreTotal = 0;
        private int opponentScoreTotal = 0;

        private Area ChangeDefence(Area oldDefence)
        {
            return (oldDefence == Area.HookKick) ? Area.HookPunch : Area.HookKick;
        }

        private Area CreateRandomAttack()
        {
            return new Random().NextDouble() > 0.5d ? Area.LowKick : Area.HookPunch;
        }

        public override MoveCollection NextMove(RoundContext context)
        {
            myScoreTotal += context.MyDamage;
            opponentScoreTotal += context.OpponentDamage;

            context.MyMoves
                .AddAttack(attack1)
                .AddAttack(attack2);

            if (context.LastOpponentMoves?.GetAttacks().Any(x => x.Area == defence) == false)
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
