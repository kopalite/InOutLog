using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InOutLog.Core
{
    public partial class AuthViewModel : ViewModelBase
    {
        private IAuthManager _authManager;
        private IDialog _dialog;

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

        public AuthViewModel(IAuthManager authManager, IDialog dialog)
        {
            _authManager = authManager;
            _dialog = dialog;
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

            await _authManager.SignInUserAsync();
            if (_authManager.AuthData.IsAuthenticated)
            {
                ViewManager.ChangeView(ViewType.Ready);
            }
            else
            {
                await _dialog.AlertAsync("Alert", _authManager.AuthData.Error);
            }

            ViewManager.SetBusy(false);
        }
    }
}
