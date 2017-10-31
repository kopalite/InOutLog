using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InOutLog.Core
{
    public partial class AuthViewModel : ViewModelBase
    {
        private IAuthManager _authManager;        

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

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _username = value;
                NotifyPropertyChanged(() => Password);
            }
        }

        public AuthViewModel(IAuthManager authManager)
        {
            _authManager = authManager;
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
            ScreenViewModel.ChangeScreen(Screen.Busy);

            var authData = await _authManager.SignInUserAsync(Username, Password);
            if (authData.Error != null)
            {
                var dialog = Externals.Resolve<IDialog>();
                await dialog.AlertAsync("Alert", authData.Error);
            }

            ScreenViewModel.ChangeScreen(Screen.Ready);
        }

        private ICommand _signUpCommand;
        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ??
                  (_signUpCommand = RegisterCommand(new RelayCommand(async x => await SignUpCommandAsync(), x => true)));
            }
        }

        public async Task SignUpCommandAsync()
        {
            ScreenViewModel.ChangeScreen(Screen.Busy);

            var authData = await _authManager.SignUpUserAsync(Username, Password);
            if (authData.Error != null)
            {
                var dialog = Externals.Resolve<IDialog>();
                await dialog.AlertAsync("Alert", authData.Error);
            }

            ScreenViewModel.ChangeScreen(Screen.Ready);
        }
    }
}
