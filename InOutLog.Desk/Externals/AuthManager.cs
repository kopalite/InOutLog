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

        public AuthData AuthData { get; private set; }

        public AuthManager(IConfig config) 
        {
            _config = config;
        }

        public async Task SignInUserAsync()
        {

            AuthData authData = null;
            
            var domain = (await _config.GetAuthDomainAsync());
            var clientId = await _config.GetAuthClientIdAsync();
            var client = new Auth0Client(new Auth0ClientOptions { Domain = domain, ClientId = clientId });
            LoginResult result = null;

            try
            {
                result = await client.LoginAsync();

                if (result.IsError)
                {
                    authData = new AuthData("Login error!", false, null, null, string.Format("Error occured: {0}", result.Error));
                }
                else
                {
                    authData = new AuthData(result.User.Identity.Name, true, result.AccessToken, result.AccessTokenExpiration, null);
                }
            }
            catch (Exception ex)
            {
                authData = new AuthData("Login error!", false, null, null, string.Format("Error occured: {0}", ex.Message));
            }

            AuthData = authData;
        }
    }
}
