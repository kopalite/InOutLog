#define DESK
using System.Threading.Tasks;
using System.Windows.Input;

namespace InOutLog.Core
{
    public partial class AuthViewModel : ViewModelBase
    {
        private IAuthManager _authManager;

        private ViewManager _viewManager;
        public ViewManager ViewManager
        {
            get { return _viewManager; }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyPropertyChanged(() => Username);
            }
        }

        public AuthViewModel()
        {
            _viewManager = new ViewManager();
            _authManager = Externals.Resolve<IAuthManager>();
        }
    }

    public partial class AuthViewModel
    {
        private ICommand _signInCommand;
        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ??
                  (_signInCommand = RegisterCommand(new RelayCommand(async x => await SignInCommandAsync(), x => true)));
            }
        }

        public async Task SignInCommandAsync()
        {
            ViewManager.SetBusy(true);

            await _authManager.StartSignInAsync();

#if DESK
            await _authManager.AfterSignInAsync();
#endif

            ViewManager.SetBusy(false);
        }
    }
}
