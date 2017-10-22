using System;

namespace InOutLog.Core
{
    public class StartedState : WatcherStateBase
    {
        public static int FactoryStateId { get { return 2; } }

        public override int StateId { get { return 2; } }

        public StartedState(WatcherData data) : base(data)
        {

        }

        public override bool CanCheckOut
        {
            get { return true; }
        }

        public override IWatcherState CheckOut()
        {
            Data.StoppedAt = DateTime.UtcNow;
            return new StoppedState(Data);
        }

        public override bool CanBreakIn
        {
            get { return true; }
        }

        public override IWatcherState BreakIn()
        {
            Data.BreakStartedAt = DateTime.UtcNow;
            return new StartedBreakState(Data);
        }
        
        public override TimeSpan GetCheckInTime()
        {
            return DateTime.UtcNow - Data.StartedAt.Value;
        }

        public override TimeSpan GetCurrentBreakInTime()
        {
            return TimeSpan.Zero;
        }

        public override TimeSpan GetTotalBreakInTime()
        {
            return Data.BreaksTotal;
        }
    }
}
