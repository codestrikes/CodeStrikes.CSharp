using System;

namespace CodeCompetition.Sdk
{
    public abstract class BotBase : MarshalByRefObject, IDisposable
    {
        public abstract MoveCollection NextMove(RoundContext context);

        public virtual void Dispose()
        {
        }
    }
}