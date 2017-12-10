using Auth0.OidcClient;
using IdentityModel.OidcClient;
using InOutLog.Core;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InOutLog.Desk
{
    public class AuthManager : IAuthManager
    {
        private IDialog _dialog;

        public AuthData AuthData { get; private set; }

        public AuthManager() 
        {
            _dialog = Externals.Resolve<IDialog>();
        }

        public async Task StartSignInAsync(params object[] args)
        {

            AuthData authData = null;
            
            var domain = Settings.AuthDomain;
            var clientId = Settings.AuthClientId;
            var audience = Settings.AuthAudience;
            var client = new Auth0Client(new Auth0ClientOptions { Domain = domain, ClientId = clientId });

            try
            {
                var result = await client.LoginAsync(new { audience = audience });

                if (result.IsError)
                {
                    authData = new AuthData("Login error!", false, null, null, string.Format("Error occured: {0}", result.Error));
                }
                else
                {
                    authData = new AuthData(GetAuthUsername(result.User), true, result.AccessToken, result.AccessTokenExpiration, null);
                }
            }
            catch (Exception ex)
            {
                authData = new AuthData("Login error!", false, null, null, string.Format("Error occured: {0}", ex.Message));
            }

            AuthData = authData;
        }

        public async Task AfterSignInAsync(params object[] args)
        {
            if (AuthData.IsAuthenticated)
            {
                ViewManager.ChangeView(ViewType.Ready);
            }
            else
            {
                await _dialog.AlertAsync("Alert", AuthData.Error);
            }
        }

        private string GetAuthUsername(ClaimsPrincipal claims)
        {
            return claims.FindFirst(x => x.Type == "sub").Value;
        }
    }
}
