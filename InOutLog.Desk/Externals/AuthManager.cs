using Auth0.OidcClient;
using IdentityModel.OidcClient;
using InOutLog.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InOutLog.Desk
{
    public class AuthManager : IAuthManager
    {
        private IConfig _config;

        private AuthData _authData;

        public AuthManager(IConfig config) 
        {
            _config = config;
        }

        public async Task<AuthData> GetAuthDataAsync()
        {
            if (_authData != null)
            {
                return _authData;
            }
            return (_authData = await SignInUserAsync());
        }

        public async Task<AuthData> SignInUserAsync()
        {
            if (_authData != null)
            {
                return _authData;
            }

            AuthData retVal = null;
            
            var domain = (await _config.GetAuthDomainAsync());
            var clientId = await _config.GetAuthClientIdAsync();
            var client = new Auth0Client(new Auth0ClientOptions { Domain = domain, ClientId = clientId });
            LoginResult result = null;

            try
            {
                result = await client.LoginAsync();

                if (result.IsError)
                {
                    retVal = new AuthData("Login error!", false, null, DateTime.MinValue, string.Format("Error occured: {0}", result.Error));
                }
                else
                {
                    retVal = new AuthData(result.User.Identity.Name, true, result.IdentityToken, result.AccessTokenExpiration, null);
                    ViewManager.ChangeView(ViewType.Ready);
                }
            }
            catch (Exception ex)
            {
                retVal = new AuthData("Login error!", false, null, DateTime.MinValue, string.Format("Error occured: {0}", ex.Message));
            }

            return retVal;
            
        }
    }
}
