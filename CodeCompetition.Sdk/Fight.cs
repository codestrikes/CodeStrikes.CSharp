using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeCompetition.Sdk
{
    public class Fight
    {
        private readonly BotBase bot1, bot2;
        private readonly IGameLogic gameLogic;

        public Fight(BotBase bot1, BotBase bot2, IGameLogic gameLogic)
        {
            this.bot1 = bot1;
            this.bot2 = bot2;
            this.gameLogic = gameLogic;
        }

        public FightResults Execute()
        {
            ReadonlyMoveCollection f1Move = null;
            ReadonlyMoveCollection f2Move = null;

            int score1 = 0;
            int score2 = 0;
            int round = 0;

            int f1Lifepoints, f2Lifepoints = f1Lifepoints = gameLogic.LifePoints;

            while (f1Lifepoints > 0 && f2Lifepoints > 0)
            {
                round++;

                RoundContext bot1Context = new RoundContext(f2Move, score1, score2);

                MoveCollection moves = null;
                bool result;

                try
                {
                    result = Task.Run(() => moves = bot1.NextMove(bot1Context), CancellationToken.None)
                        .Wait(gameLogic.MaxMoveTime);
                    bot1Context.SetMoves(moves);
                }
                catch (Exception exc)
                {
                    return FightResults.Error(FightResultErrorType.Runtime, FightResultType.Lost, exc.Message);
                }
                if (!result)
                    return FightResults.Error(FightResultErrorType.Timeout, FightResultType.Lost, bot1 + " exceeded move timeout");                    
                if (gameLogic.Validate(moves) == false)
                    return FightResults.Error(FightResultErrorType.IllegalMove, FightResultType.Lost, bot1 + " made an illegal move");
                

                RoundContext bot2Context = new RoundContext(f1Move, score2, score1);
                try
                {
                    result = Task.Run(() => moves = bot2.NextMove(bot2Context), CancellationToken.None)
                        .Wait(gameLogic.MaxMoveTime);
                    bot2Context.SetMoves(moves);
                }
                catch (Exception exc)
                {
                    return FightResults.Error(FightResultErrorType.Runtime, FightResultType.Win, exc.Message);
                }
                if (!result)
                    return FightResults.Error(FightResultErrorType.Timeout, FightResultType.Win, bot2 + " exceeded move timeout");
                if (gameLogic.Validate(moves) == false)
                    return FightResults.Error(FightResultErrorType.IllegalMove, FightResultType.Win, bot2 + " made an illegal move");
                
                f1Move = bot1Context.MyMoves;
                f2Move = bot2Context.MyMoves;

                score1 = gameLogic.CalculateScore(f1Move, f2Move);
                score2 = gameLogic.CalculateScore(f2Move, f1Move);

                f1Lifepoints -= score2;
                f2Lifepoints -= score1;

                if (!gameLogic.ValidateRound(round, f1Lifepoints, f2Lifepoints))
                {
                    return FightResults.Draw(f1Lifepoints, f2Lifepoints);
                }
            }

            if (f1Lifepoints > f2Lifepoints)
            {
                return FightResults.Win(f1Lifepoints, f2Lifepoints);
            }
            else if (f1Lifepoints == f2Lifepoints)
            {
                return FightResults.Draw(f1Lifepoints, f2Lifepoints);
            }
            return FightResults.Lost(f1Lifepoints, f2Lifepoints);
        }
    }

    public enum FightExceptionReason
    {
        Timeout,
        IllegalMove,
        BotException
    }

    public class FightException : Exception
    {
        public FightExceptionReason Reason { get; }
        public BotBase ErrorBot { get; }

        public FightException(string message, FightExceptionReason reason, BotBase errorBot, Exception innerException = null) : base(message, innerException)
        {
            Reason = reason;
            ErrorBot = errorBot;
        }
    }
}
