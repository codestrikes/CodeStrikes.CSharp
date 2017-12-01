using System;

using System.Linq;

using CodeCompetition.Sdk;

namespace CodeCompetition.TestingApp
{
    class PlayerBot : BotBase
    {
        private Area attack1 = Area.Head;
        private Area attack2 = Area.Sensors;
        private Area defence = Area.Sensors;

        public override MoveCollection NextMove(RoundContext context)
        {            
            context.MyMoves
                .AddAttack(attack1)
                .AddAttack(attack2)
                .AddDefence(defence);
            return context.MyMoves;
        }

        public override string ToString()
        {
            return "Player";
        }
    }
}
