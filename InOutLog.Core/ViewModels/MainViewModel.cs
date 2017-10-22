using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace InOutLog.Core
{
    public partial class MainViewModel : ViewModelBase
    {
        private ILogPersister _persister;
        private Timer _refresher;
        private int _syncCounter;

        public string Title
        {
            get
            {
                string mode = _persister.IsLocalMode ? "local" : "remote";
                return string.Format("In/Out Log - {0} ({1} mode)", DateTime.UtcNow.ToString(Entry.TodayDateFormat), mode);
            }
        }

        private InOutWatcher _watcher;
        public InOutWatcher Watcher
        {
            get { return _watcher; }
            private set
            {
                _watcher = value;
                NotifyPropertyChanged(() => Watcher);
            }
        }

        private bool _isInitialized;
        public bool IsInitialized
        {
            get { return _isInitialized && Watcher != null; }
            private set
            {
                _isInitialized = value;
                NotifyPropertyChanged(() => IsInitialized);
            }
        }

        public MainViewModel()
        {
            _persister = PersisterFactory.Create();
            _refresher = new Timer(x =>
            {
                var safeUI = Externals.Resolve<ISafeUI>();
                safeUI.Invoke(new Action(async () =>
                {
                    _syncCounter++;

                    var refreshInterval = await Config.GetRefreshIntervalAsync();

                    if (_syncCounter > 0 && _syncCounter % refreshInterval == 0)
                    {
                        await Sync();
                    }

                    RaiseAllPropertyChanged();
                }));
            }, 
            null, 0, 1000);
        }

        private async Task Sync()
        {
            Entry entry = await _persister.RestoreAsync();
            if (entry != null && entry.SessionId != Session.SessionId)
            {
                var state = StateFactory.Create(entry.StateId, entry.Data);
                Watcher.ChangeState(() => state);
            }
        }

        #region [ IDisposable ]

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _refresher.Dispose();
            }
            _disposed = true;
        }

        #endregion
    }

    public partial class MainViewModel
    {
        private ICommand _initCommand;
        public ICommand InitCommand
        {
            get { return _initCommand ?? (_initCommand = RegisterCommand(new RelayCommand(async x => await Init(), x => true))); }
        }

        private async Task Init()
        {
            var entry = await _persister.RestoreAsync();
            var state = entry == null ? StateFactory.Create() : StateFactory.Create(entry.StateId, entry.Data);
            Watcher = new InOutWatcher(_persister, state);
            IsInitialized = true;
        } 
    }
}
