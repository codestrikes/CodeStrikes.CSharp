using System;

namespace CodeCompetition.Sdk
{
    public abstract class BotBase : MarshalByRefObject, IDisposable
    {
        public abstract MoveCollection NextMove(RoundContext context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

    }
}