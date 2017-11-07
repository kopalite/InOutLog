namespace InOutLog.Core
{
    public enum ViewType
    {
        Auth = 0,
        Ready = 2
    }

    public class ViewManager : ViewModelBase
    {
        private static ViewManager _instance;

        private static ViewType _screen;

        public bool IsAuth
        {
            get { return _screen == ViewType.Auth; }
        }

        public bool IsReady
        {
            get { return _screen == ViewType.Ready; }
        }

        public bool IsBusy { get; private set; }

        public ViewManager()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public static void ChangeView(ViewType screen)
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
