using System;
using System.Linq;

namespace CodeCompetition.Sdk.Bots
{
    public class Kickboxer : BotBase
    {
        private Area attack1 = Area.Head;
        private Area attack2 = Area.Sensors;
        private Area defence = Area.Sensors;
    
        private Area CreateRandomArea() 
        {
            double random = new Random().NextDouble();
            if (random<0.3)
                return Area.Sensors;

            if (random<0.7)
                return Area.Head;

            if (random<0.9)
                return Area.Belly;

            return Area.Belly;
        }

        public override MoveCollection NextMove(RoundContext context)
        {
            if (context.LastOpponentMoves?.Defences.Any(x => x.Area == this.attack1) == true)
            {
                this.attack1 = CreateRandomArea();
            }

            this.attack2 = CreateRandomArea();

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
