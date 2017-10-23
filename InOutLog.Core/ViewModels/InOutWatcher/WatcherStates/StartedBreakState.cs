using System;

namespace InOutLog.Core
{
    public class StartedBreakState : WatcherStateBase
    {
        public static int FactoryStateId { get { return 3; } }

        public override int StateId { get { return 3; } }

        public StartedBreakState(WatcherData data) : base(data)
        {

        }

        public override bool CanBreakOut
        {
            get { return true; }
        }

        public override IWatcherState BreakOut()
        {
            Data.BreakStoppedAt = DateTime.UtcNow;
            Data.BreaksTotal += (DateTime.UtcNow - Data.BreakStartedAt.Value);
            return new StartedState(Data);
        }

        public override TimeSpan GetCheckInTime()
        {
            return DateTime.UtcNow - Data.StartedAt.Value;
        }

        public override TimeSpan GetBreakInTime()
        {
            return Data.BreaksTotal + (DateTime.UtcNow - Data.BreakStartedAt.Value);
        }
    }
}
