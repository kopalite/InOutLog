using System;

namespace InOutLog.Core
{
    public class IdleState : WatcherStateBase
    {
        public static int FactoryStateId { get { return 1; } }

        public override int StateId { get { return 1; } }

        public IdleState(WatcherData data) : base(data)
        {

        }

        public override bool CanCheckIn
        {
            get { return true; }
        }
        
        public override IWatcherState CheckIn()
        {
            Data.StartedAt = DateTime.UtcNow;
            return new StartedState(Data);
        }

        public override TimeSpan GetCheckInTime()
        {
            return TimeSpan.Zero;
        }

        public override TimeSpan GetBreakInTime()
        {
            return TimeSpan.Zero;
        }
    }
}
