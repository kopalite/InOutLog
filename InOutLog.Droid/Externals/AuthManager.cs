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
        private IConfig _config;

        public AuthData AuthData { get; private set; }

        public AuthManager(IConfig config) 
        {
            _config = config;
        }

        public async Task SignInUserAsync()
        {

            AuthData authData = null;
            
            var domain = await _config.GetAuthDomainAsync();
            var clientId = await _config.GetAuthClientIdAsync();
            var audience = await _config.GetAuthAudienceAsync();
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

        private string GetAuthUsername(ClaimsPrincipal claims)
        {
            return claims.FindFirst(x => x.Type == "sub").Value;
        }
    }
}
