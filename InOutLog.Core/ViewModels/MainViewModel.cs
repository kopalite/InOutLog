using System;


namespace InOutLog.Core
{
    public partial class MainViewModel : ViewModelBase
    {
        private IConfig _config;
        private IAuthManager _authManager;
        private ILogPersister _persister;
        private Timer _refresher;
        private int _syncCounter;

        public string Title
        {
            get
            {
                string mode = _persister.IsLocalMode ? "local" : "remote";
                return string.Format("In/Out Log - {0} ({1} mode)", Entry.GetDisplayDate(), mode);
            }
        }

        private AuthViewModel _authViewModel;
        public AuthViewModel AuthViewModel
        {
            get { return _authViewModel; }
            private set
            {
                _authViewModel = value;
                NotifyPropertyChanged(() => AuthViewModel);
            }
        }

        private ScreenManager _screenManager;
        public ScreenManager ScreenManager
        {
            get { return _screenManager; }
            private set
            {
                _screenManager = value;
                NotifyPropertyChanged(() => ScreenManager);
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
        
        public MainViewModel()
        {
            _config = Externals.Resolve<IConfig>();
            _authManager = Externals.Resolve<IAuthManager>();
            _persister = PersisterFactory.Create();
            _refresher = new Timer(x =>
            {
                var safeUI = Externals.Resolve<ISafeUI>();
                safeUI.Invoke(new Action(async () =>
                {
                    if (!ScreenManager.IsReady)
                    {
                        return;
                    }

                    _syncCounter++;

                    var interval = await _config.GetRefreshIntervalAsync();

                    if (_syncCounter > 0 && _syncCounter % interval.Seconds == 0)
                    {
                        await Watcher.RestoreStateAsync();
                    }

                    RaiseAllPropertyChanged();
                }));
            }, 
            null, 0, 1000);

            AuthViewModel = new AuthViewModel(_authManager);
            ScreenManager = new ScreenManager();
            Watcher = new InOutWatcher(_persister);
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
}
