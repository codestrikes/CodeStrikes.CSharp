using System.Collections.Generic;

namespace CodeStrikes.Sdk
{

    public class FightResultError
    {
        public FightResultError()
        {

        }
        public FightResultError(FightResultErrorType errorType, string stackTrace, string message)
        {
            ErrorType = errorType;
            StackTrace = stackTrace;
            Message = message;
        }
        public FightResultError(FightResultErrorType errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }

        public FightResultErrorType ErrorType { get; set; }
        public string StackTrace { get; set; }
        public string Message { get; set; }
    }
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
        public FightResultError Error { get; }
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

        private FightResults(FightResultError error, FightResultType result) : this()
        {
            Error = error;
            Result = result;
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

        public static FightResults SetError(FightResultError error, FightResultType result)
        {
            return new FightResults(error, result);
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
                    $"{Result.ToString()} with error {nameof(Error.ErrorType)}: {Error.ErrorType.ToString()}, {nameof(Error.Message)}: {Error.Message}";
            }

            return
                $"{Result.ToString()}: {nameof(PlayerScore)}: {PlayerScore}, {nameof(OpponentScore)}: {OpponentScore}";
        }
    }
}