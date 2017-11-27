using System;


namespace InOutLog.Core
{
    public partial class MainViewModel : ViewModelBase
    {
        private IConfig _config;
        private IDialog _dialog;
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

        private IAuthManager _authManager;
        public IAuthManager AuthManager
        {
            get { return _authManager; }
        }

        private ViewManager _viewManager;
        public ViewManager ViewManager
        {
            get { return _viewManager; }
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
            _viewManager = new ViewManager();

            _config = Externals.Resolve<IConfig>();
            _authManager = Externals.Resolve<IAuthManager>();
            _dialog = Externals.Resolve<IDialog>();
            _persister = new RemotePersister(_config, _authManager);
            _refresher = new Timer(x =>
            {
                var safeUI = Externals.Resolve<ISafeUI>();
                safeUI.Invoke(new Action(async () =>
                {
                    if (!ViewManager.IsReady)
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
            
            Watcher = new InOutWatcher(_config, _authManager, _dialog, _persister);
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
