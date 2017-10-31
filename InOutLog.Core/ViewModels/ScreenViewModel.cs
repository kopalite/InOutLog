namespace InOutLog.Core
{
    public enum Screen
    {
        Auth = 0,
        Busy = 1,
        Ready = 2
    }

    public class ScreenViewModel : ViewModelBase
    {
        private static ScreenViewModel _instance;

        private static Screen _screen;

        public bool IsAuth
        {
            get { return _screen == Screen.Auth; }
        }

        public bool IsBusy
        {
            get { return _screen == Screen.Busy; }
        }

        public bool IsReady
        {
            get { return _screen == Screen.Ready; }
        }

        public ScreenViewModel()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public static void ChangeScreen(Screen screen)
        {
            _screen = screen;

            _instance.RaiseAllPropertyChanged();
        }
    }
}
