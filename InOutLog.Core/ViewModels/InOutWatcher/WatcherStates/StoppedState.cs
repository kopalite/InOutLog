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
        
        public override TimeSpan GetCheckInTime()
        {
            return Data.StoppedAt.Value - Data.StartedAt.Value;
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
