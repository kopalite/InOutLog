using System;

namespace InOutLog.Core
{
    public abstract class WatcherStateBase : IWatcherState
    {
        public abstract int StateId { get; }

        public WatcherData Data { get; private set; }

        public WatcherStateBase(WatcherData data)
        {
            Data = data;

            data.SetState(this);
        }

        public virtual IWatcherState CheckIn()
        {
            return this;
        }

        public virtual bool CanCheckIn
        {
            get { return false; }
        }

        public virtual IWatcherState CheckOut()
        {
            return this;
        }

        public virtual bool CanCheckOut
        {
            get { return false; }
        }

        public virtual IWatcherState BreakIn()
        {
            return this;
        }

        public virtual bool CanBreakIn
        {
            get { return false; }
        }

        public virtual IWatcherState BreakOut()
        {
            return this;
        }

        public virtual bool CanBreakOut
        {
            get { return false; }
        }

        public abstract TimeSpan GetCheckInTime();

        public abstract TimeSpan GetCurrentBreakInTime();

        public abstract TimeSpan GetTotalBreakInTime();
    }
}
