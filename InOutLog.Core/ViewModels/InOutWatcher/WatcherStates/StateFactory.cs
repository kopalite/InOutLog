using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public class StateFactory
    {
        private static Dictionary<int, Func<WatcherData, IWatcherState>> _creators;

        static StateFactory()
        {
            _creators = new Dictionary<int, Func<WatcherData, IWatcherState>>();
            _creators.Add(IdleState.FactoryStateId, x => new IdleState(x));
            _creators.Add(StartedState.FactoryStateId, x => new StartedState(x));
            _creators.Add(StartedBreakState.FactoryStateId, x => new StartedBreakState(x));
            _creators.Add(StoppedState.FactoryStateId, x => new StoppedState(x));
        }
        
        public static IWatcherState Create()
        {
            return _creators[IdleState.FactoryStateId](new WatcherData());
        }

        public static IWatcherState Create(int stateId, WatcherData data)
        {
            return _creators[stateId](data);
        }
    }
}
