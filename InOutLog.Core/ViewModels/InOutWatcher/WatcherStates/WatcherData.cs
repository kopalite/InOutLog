using Newtonsoft.Json;
using System;

namespace InOutLog.Core
{
    public class WatcherData : ViewModelBase
    {
        #region [ Checkin & Breaks ]

        private DateTime? _startedAt;
        public DateTime? StartedAt
        {
            get { return _startedAt; }
            set
            {
                _startedAt = value.ToUtc();
                NotifyPropertyChanged(() => StartedAt);
            }
        }

        private DateTime? _stoppedAt;
        public DateTime? StoppedAt
        {
            get { return _stoppedAt; }
            set
            {
                _stoppedAt = value.ToUtc();
                NotifyPropertyChanged(() => StoppedAt);
            }
        }

        private DateTime? _breakStartedAt;
        public DateTime? BreakStartedAt
        {
            get { return _breakStartedAt; }
            set
            {
                _breakStartedAt = value.ToUtc();
                NotifyPropertyChanged(() => BreakStartedAt);
            }
        }

        private DateTime? _breakStoppedAt;
        public DateTime? BreakStoppedAt
        {
            get { return _breakStoppedAt; }
            set
            {
                _breakStoppedAt = value.ToUtc();
                NotifyPropertyChanged(() => BreakStoppedAt);
            }
        }

        private TimeSpan _breaksTotal;
        public TimeSpan BreaksTotal
        {
            get { return _breaksTotal; }
            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw new Exception("BreaksTotal should always be positive value or 0!");
                }

                _breaksTotal = value;
                NotifyPropertyChanged(() => BreaksTotal);
            }
        }

        #endregion

        #region [ Computed props ]

        [JsonIgnore]
        public TimeSpan CheckInTime
        {
            get { return _state.GetCheckInTime(); }
        }

        [JsonIgnore]
        public TimeSpan BreakInTime
        {
            get { return _state.GetBreakInTime(); }
        }

        [JsonIgnore]
        public TimeSpan WorkingTime
        {
            get { return _state.GetCheckInTime() - _state.GetBreakInTime(); }
        }

        #endregion

        #region [ ctor, refresh, state ]

        

        private IWatcherState _state;

        public WatcherData()
        {
            _breaksTotal = TimeSpan.Zero;
        }
        
        /// <summary>
        /// This method should be called immediatelly after the WatcherData instance creation. 
        /// It depends on watcher state and has a mutual relation with it.
        /// </summary>
        /// <param name="state">Watcher state instance that is associated with this data instance.</param>
        public void SetState(IWatcherState state)
        {
            _state = state;
        }

        #endregion
    }
}
