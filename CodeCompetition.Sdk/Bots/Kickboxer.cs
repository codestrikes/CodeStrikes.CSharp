using System;
using System.Linq;

namespace CodeStrikes.Sdk.Bots
{
    public class Kickboxer : BotBase
    {
        private Area attack1 = Area.HookPunch;
        private readonly Area defence = Area.HookKick;
    
        private Area CreateRandomArea() 
        {
            double random = new Random().NextDouble();
            if (random<0.3)
                return Area.HookKick;

            if (random<0.7)
                return Area.HookPunch;

            if (random<0.9)
                return Area.LowKick;

            return Area.LowKick;
        }

        public override MoveCollection NextMove(RoundContext context)
        {
            if (context.LastOpponentMoves?.GetDefences().Any(x => x.Area == this.attack1) == true)
            {
                this.attack1 = CreateRandomArea();
            }

            var attack2 = CreateRandomArea();

            context.MyMoves
                .AddAttack(attack1)
                .AddAttack(attack2)
                .AddDefence(defence);
            return context.MyMoves;
        }

        public override string ToString()
        {
            return "Kickboxer";
        }
    }
}
