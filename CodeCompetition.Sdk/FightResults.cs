using System.Collections.Generic;

namespace CodeCompetition.Sdk
{
    public enum FightResultErrorType
    {
        Timeout,
        Runtime,
        IllegalMove
    }

    public enum FightResultType
    {
        Win,
        Lost,
        Draw
    }

    public class RoundResult
    {
        public int RoundNumber { get; }
        public ReadonlyMoveCollection PlayerMoves { get; }
        public ReadonlyMoveCollection OpponentMoves { get; }
        public int PlayerScore { get; }
        public int OpponentScore { get; }

        public RoundResult(int roundNumber, ReadonlyMoveCollection playerMoves, ReadonlyMoveCollection opponentMoves, int playerScore, int opponentScore)
        {
            RoundNumber = roundNumber;
            PlayerMoves = playerMoves;
            OpponentMoves = opponentMoves;
            PlayerScore = playerScore;
            OpponentScore = opponentScore;            
        }

        public override string ToString()
        {
            return
                $"Round {RoundNumber} - Player points: {PlayerScore}, Opponent points: {OpponentScore}\nPlayer: {PlayerMoves}\nOpponent: {OpponentMoves}";
        }
    }

    public class FightResults
    {        
        public int PlayerScore { get; }
        public int OpponentScore { get; }
        public FightResultType Result { get; }
        public bool IsError { get; }
        public string ErrorMessage { get; }
        public FightResultErrorType? ErrorType { get; }
        public List<RoundResult> RoundResults { get; private set; }

        private FightResults()
        {
            RoundResults = new List<RoundResult>();
        }

        private FightResults(int playerScore, int opponentScore, FightResultType result) : this()
        {
            PlayerScore = playerScore;
            OpponentScore = opponentScore;
            Result = result;
        }

        private FightResults(FightResultErrorType errorType, FightResultType result, string errorMessage) : this()
        {
            ErrorType = errorType;
            Result = result;
            ErrorMessage = errorMessage;
            IsError = true;
        }

        public static FightResults Draw(int playerScore, int opponentScore)
        {
            return new FightResults(playerScore, opponentScore, FightResultType.Draw);
        }

        public static FightResults Win(int playerScore, int opponentScore)
        {
            return new FightResults(playerScore, opponentScore, FightResultType.Win);
        }

        public static FightResults Lost(int playerScore, int opponentScore)
        {
            return new FightResults(playerScore, opponentScore, FightResultType.Lost);
        }

        public static FightResults Error(FightResultErrorType errorType, FightResultType result, string errorMessage)
        {
            return new FightResults(errorType, result, errorMessage);
        }

        public FightResults SetRoundResults(List<RoundResult> results)
        {
            RoundResults = results;
            return this;
        }

        public override string ToString()
        {
            if (IsError)
            {
                return
                    $"{Result.ToString()} with error {nameof(ErrorType)}: {ErrorType.ToString()}, {nameof(ErrorMessage)}: {ErrorMessage}";
            }

            return
                $"{Result.ToString()}: {nameof(PlayerScore)}: {PlayerScore}, {nameof(OpponentScore)}: {OpponentScore}";
        }
    }
}