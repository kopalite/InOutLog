using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InOutLog.Core
{
    public partial class InOutWatcher : ViewModelBase
    {
        private ILogPersister _persister;

        public IWatcherState State { get; private set; }

        public InOutWatcher(ILogPersister persister, IWatcherState state)
        {
            _persister = persister;
            State = state;
        }

        public async Task CheckIn()
        {
            ChangeState(() => State.CheckIn());
            await PersistAsync();
        }

        public bool CanCheckIn
        {
            get { return State.CanCheckIn; }
        }

        public async Task CheckOut()
        {
            ChangeState(() => State.CheckOut());
            await PersistAsync();
        }

        public bool CanCheckOut
        {
            get { return State.CanCheckOut; }
        }

        public async Task BreakIn()
        {
            ChangeState(() => State.BreakIn());
            IsInBreak = true;
            await PersistAsync();
        }

        public bool CanBreakIn
        {
            get { return State.CanBreakIn; }
        }

        public async Task BreakOut()
        {
            ChangeState(() => State.BreakOut());
            IsInBreak = false;
            await PersistAsync();
        }

        public bool CanBreakOut
        {
            get { return State.CanBreakOut; }
        }

        private bool _isInBreak;
        public bool IsInBreak
        {
            get { return _isInBreak; }
            private set
            {
                _isInBreak = value;
                NotifyPropertyChanged(() => IsInBreak);
            }
        }

        public bool IsStopped
        {
            get { return State is StoppedState; }
        }

        internal void ChangeState(Func<IWatcherState> action)
        {
            State = action();
            RaiseAllCanExecute();
            RaiseAllPropertyChanged();
        }

        private async Task PersistAsync()
        {
            var entry = await Entry.CreateAsync(State.StateId, State.Data);
            await _persister.PersistAsync(entry);
        }
    }

    public partial class InOutWatcher
    {
        private RelayCommand _checkInCommand;
        public ICommand CheckInCommand
        {
            get { return _checkInCommand ?? (_checkInCommand = RegisterCommand(new RelayCommand(async x => await CheckIn(), x => CanCheckIn))); }
        }

        private RelayCommand _checkOutCommand;
        public ICommand CheckOutCommand
        {
            get { return _checkOutCommand ?? (_checkOutCommand = RegisterCommand(new RelayCommand(async x => await CheckOut(), x => CanCheckOut))); }
        }

        private RelayCommand _breakInCommand;
        public ICommand BreakInCommand
        {
            get { return _breakInCommand ?? (_breakInCommand = RegisterCommand(new RelayCommand(async x => await BreakIn(), x => CanBreakIn))); }
        }

        private RelayCommand _breakOutCommand;
        public ICommand BreakOutCommand
        {
            get { return _breakOutCommand ?? (_breakOutCommand = RegisterCommand(new RelayCommand(async x => await BreakOut(), x => CanBreakOut))); }
        }
    }
}
