namespace InOutLog.Core
{
    public enum Screen
    {
        Auth = 0,
        Ready = 2
    }

    public class ScreenManager : ViewModelBase
    {
        private static ScreenManager _instance;

        private static Screen _screen;

        public bool IsAuth
        {
            get { return _screen == Screen.Auth; }
        }

        public bool IsReady
        {
            get { return _screen == Screen.Ready; }
        }

        public bool IsBusy { get; private set; }

        public ScreenManager()
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

        public static void SetBusy(bool isBusy)
        {
            _instance.IsBusy = isBusy;
            _instance.RaiseAllPropertyChanged();
        }
    }
}
