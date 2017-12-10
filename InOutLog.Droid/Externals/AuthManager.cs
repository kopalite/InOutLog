using Android.App;
using Android.Content;
using Auth0.OidcClient;
using IdentityModel.OidcClient;
using InOutLog.Core;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InOutLog.Droid
{
    public class AuthManager : IAuthManager
    {
        private IDialog _dialog;

        private Auth0Client _authClient;

        private AuthorizeState _authState;

        public AuthData AuthData { get; private set; }

        public AuthManager()
        {
            _dialog = Externals.Resolve<IDialog>();
        }

        public async Task StartSignInAsync(params object[] args)
        {
            AuthData authData = null;
            
            var domain =   Settings.AuthDomain;
            var clientId = Settings.AuthClientId;
            var audience = Settings.AuthAudience;
            var activity = args[0] as Activity;

            _authClient = new Auth0Client(new Auth0ClientOptions
            {
                Domain = domain,
                ClientId = clientId,
                Activity = activity
            });

            try
            {
                _authState = await _authClient.PrepareLoginAsync(new { audience = audience });
                var uri = Android.Net.Uri.Parse(_authState.StartUrl);
                var intent = new Intent(Intent.ActionView, uri);
                intent.AddFlags(ActivityFlags.NoHistory);
                activity.StartActivity(intent);
            }
            catch (Exception ex)
            {
                authData = new AuthData("Login error!", false, null, null, string.Format("Error occured: {0}", ex.Message));
            }

            AuthData = authData;
        }

        public async Task AfterSignInAsync(params object[] args)
        {
            AuthData authData = null;

            try
            {
                var intent = args[0] as Intent;
                var result = await _authClient.ProcessResponseAsync(intent.DataString, _authState);
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
