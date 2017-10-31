using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InOutLog.Core
{
    public partial class InOutWatcher : ViewModelBase
    {
        private IConfig _config;
        private ILogPersister _persister;

        public IWatcherState State { get; private set; }        

        public InOutWatcher(ILogPersister persister)
        {
            _config = Externals.Resolve<IConfig>();
            _persister = persister;
        }

        public async Task CheckIn()
        {
            await ChangeStateAsync(() => State.CheckIn());
        }

        public bool CanCheckIn
        {
            get { return State != null && State.CanCheckIn; }
        }

        public async Task CheckOut()
        {
           await ChangeStateAsync(() => State.CheckOut());
        }

        public bool CanCheckOut
        {
            get { return State != null && State.CanCheckOut; }
        }

        public async Task BreakIn()
        {
            await ChangeStateAsync(() => State.BreakIn());
        }

        public bool CanBreakIn
        {
            get { return State != null && State.CanBreakIn; }
        }

        public async Task BreakOut()
        {
            await ChangeStateAsync(() => State.BreakOut());
        }

        public bool CanBreakOut
        {
            get { return State != null && State.CanBreakOut; }
        }

        public async Task Reset()
        {
            var state = await State.ResetAsync();
            await ChangeStateAsync(() => state);
        }

        public bool CanReset
        {
            get { return State != null && State.CanReset; }
        }


        private async Task ChangeStateAsync(Func<IWatcherState> action)
        {
            var entry = await _persister.RestoreAsync();
            var interval = await _config.GetRefreshIntervalAsync();
            if (entry != null && entry.SessionId != Session.SessionId && (DateTime.UtcNow - entry.Timestamp) < interval)
            {
                var dialog = Externals.Resolve<IDialog>();
                await dialog.AlertAsync("Alert", "Syncing in progress... Please try again later.");
                return;
            }

            State = action();
            RaiseAllCanExecute();
            RaiseAllPropertyChanged();

            entry = await Entry.CreateAsync(State.StateId, State.Data);
            await _persister.PersistAsync(entry);
        }

        internal async Task RestoreStateAsync()
        {
            var entry = await _persister.RestoreAsync();
            if (entry != null && entry.SessionId != Session.SessionId)
            {
                State = StateFactory.Create(entry.StateId, entry.Data);
                RaiseAllCanExecute();
                RaiseAllPropertyChanged();
            }
        }
    }

    public partial class InOutWatcher
    {
        private bool _isInitialized;

        private ICommand _startupCommand;
        public ICommand StartupCommand
        {
            get
            {
                return _startupCommand ??
                  (_startupCommand = RegisterCommand(new RelayCommand(async x => 
                  {
                      if (_isInitialized) return;
                      await StartupAsync();
                      _isInitialized = true;
                  }, x => true)));
            }
        }

        public async Task StartupAsync()
        {
            ScreenViewModel.ChangeScreen(Screen.Busy);

            var entry = await _persister.RestoreAsync();
            State = entry == null ? StateFactory.Create() : StateFactory.Create(entry.StateId, entry.Data);
            RaiseAllCanExecute();
            RaiseAllPropertyChanged();

            ScreenViewModel.ChangeScreen(Screen.Ready);
        }

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

        private RelayCommand _resetCommand;
        public ICommand ResetCommand
        {
            get { return _resetCommand ?? (_resetCommand = RegisterCommand(new RelayCommand(async x => await Reset(), x => CanReset))); }
        }
    }
}
