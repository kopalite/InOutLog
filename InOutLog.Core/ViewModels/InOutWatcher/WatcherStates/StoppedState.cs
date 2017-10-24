using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public class StoppedState : WatcherStateBase
    {
        public static int FactoryStateId { get { return 4; } }

        public override int StateId { get { return 4; } }

        public StoppedState(WatcherData data) : base(data)
        {

        }

        public override bool CanReset
        {
            get { return true; }
        }

        public override IWatcherState Reset()
        {
            var dialog = Externals.Resolve<IDialog>();
            var isConfirmed = dialog.Option("Alert", "Discard today's log?");
            if (!isConfirmed)
            {
                return this;
            }

            Data.StartedAt = null;
            Data.StoppedAt = null;
            Data.BreakStartedAt = null;
            Data.BreakStoppedAt = null;
            Data.BreaksTotal = TimeSpan.Zero;
            
            return new IdleState(Data);
        }

        public override TimeSpan GetCheckInTime()
        {
            return Data.StoppedAt.Value - Data.StartedAt.Value;
        }

        public override TimeSpan GetBreakInTime()
        {
            return Data.BreaksTotal;
        }
    }
}
