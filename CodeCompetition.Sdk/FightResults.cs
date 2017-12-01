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


    public class FightResults
    {        
        public int PlayerScore { get; }
        public int OpponentScore { get; }
        public FightResultType Result { get; }
        public bool IsError { get; }
        public string ErrorMessage { get;  }
        public FightResultErrorType? ErrorType { get; }

        private FightResults(int playerScore, int opponentScore, FightResultType result)
        {
            
            PlayerScore = playerScore;
            OpponentScore = opponentScore;
            Result = result;
        }

        public FightResults(FightResultErrorType errorType, FightResultType result, string errorMessage)
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

        public override string ToString()
        {
            if (IsError)
            {
                return $"{Result.ToString()} with error {nameof(ErrorType)}: {ErrorType.ToString()}, {nameof(ErrorMessage)}: {ErrorMessage}";
            }
            
            return $"{Result.ToString()}: {nameof(PlayerScore)}: {PlayerScore}, {nameof(OpponentScore)}: {OpponentScore}";
        }
        
    }
}