using System;

namespace InOutLog.Core
{
    public interface IWatcherState
    {
        int StateId { get; }

        WatcherData Data { get; }

        IWatcherState CheckIn();
        bool CanCheckIn { get; }

        IWatcherState CheckOut();
        bool CanCheckOut { get; }

        IWatcherState BreakIn();
        bool CanBreakIn { get; }

        IWatcherState BreakOut();
        bool CanBreakOut { get; }

        TimeSpan GetCheckInTime();
        TimeSpan GetCurrentBreakInTime();
        TimeSpan GetTotalBreakInTime();
    }
}
