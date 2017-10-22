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
                _startedAt = ToUtc(value);
                NotifyPropertyChanged(() => StartedAt);
            }
        }

        private DateTime? _stoppedAt;
        public DateTime? StoppedAt
        {
            get { return _stoppedAt; }
            set
            {
                _stoppedAt = ToUtc(value);
                NotifyPropertyChanged(() => StoppedAt);
            }
        }

        private DateTime? _breakStartedAt;
        public DateTime? BreakStartedAt
        {
            get { return _breakStartedAt; }
            set
            {
                _breakStartedAt = ToUtc(value);
                NotifyPropertyChanged(() => BreakStartedAt);
            }
        }

        private DateTime? _breakStoppedAt;
        public DateTime? BreakStoppedAt
        {
            get { return _breakStoppedAt; }
            set
            {
                _breakStoppedAt = ToUtc(value);
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

        private DateTime? ToUtc(DateTime? value)
        {
            if (!value.HasValue)
            {
                return null;
            }
            return value.Value.ToUniversalTime();
        }

        #endregion

        #region [ Computed props ]

        [JsonIgnore]
        public TimeSpan CheckInTime
        {
            get { return _state.GetCheckInTime(); }
        }

        [JsonIgnore]
        public TimeSpan CurrentBreakInTime
        {
            get { return _state.GetCurrentBreakInTime(); }
        }

        [JsonIgnore]
        public TimeSpan TotalBreakInTime
        {
            get { return _state.GetTotalBreakInTime(); }
        }

        [JsonIgnore]
        public TimeSpan WorkingTime
        {
            get { return _state.GetCheckInTime() - _state.GetTotalBreakInTime(); }
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
