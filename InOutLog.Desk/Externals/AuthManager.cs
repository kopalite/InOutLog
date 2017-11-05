using InOutLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InOutLog.Desk
{
    public class AuthManager : IAuthManager
    {
        private AuthData _authData;

        public async Task<AuthData> GetAuthDataAsync()
        {
            if (_authData != null)
            {
                return _authData;
            }
            
            await Task.Delay(1000);
            _authData = new AuthData("ivan.kopcanski");
            return _authData;
        }

        public async Task<AuthData> SignUpUserAsync(string username, string password)
        {
            /*
             var client = new RestClient("https://YOUR_AUTH0_DOMAIN/api/v2/tenants/settings");
                var request = new RestRequest(Method.PATCH);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Bearer API2_ACCESS_TOKEN");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{ \"flags\": { \"enable_dynamic_client_registration\": true } }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
             */

            //var patchClient = await GetClientAsync();
            //patchClient.DefaultRequestHeaders.CacheControl.


            //var signUpClient = await GetClientAsync();
            //var content = new FormUrlEncodedContent(new[]
            //{
            //    //"{\"client_name\":\"My Dynamic Client\",\"redirect_uris\": [\"https://client.example.com/callback\", \"https://client.example.com/callback2\"]}"
            //    new KeyValuePair<string, string>("application/json", "{ 'client_name':'testClient', 'redirect_uris' : [] }"),
            //});
            //var response = await signUpClient.PostAsync("/oidc/register", content);

            await Task.Delay(1000);
            ScreenManager.ChangeScreen(Screen.Ready);
            return new AuthData(username);
            
        }

        public async Task<AuthData> SignInUserAsync(string username, string password)
        {
            await Task.Delay(1000);
            ScreenManager.ChangeScreen(Screen.Ready);
            return new AuthData(username);
        }

        public async Task<AuthData> SignInUserAsync()
        {
            await Task.Delay(1000);
            ScreenManager.ChangeScreen(Screen.Ready);
            return new AuthData("ivan.kopcanski");
        }

        //private async Task<HttpClient> GetClientAsync()
        //{
        ////    var client = new RestClient("https://YOUR_AUTH0_DOMAIN/oidc/register");
        ////    var request = new RestRequest(Method.POST);
        ////    request.AddHeader("content-type", "application/json");
        ////    request.AddParameter("application/json", "{\"client_name\":\"My Dynamic Client\",\"redirect_uris\": [\"https://client.example.com/callback\", \"https://client.example.com/callback2\"]}", ParameterType.RequestBody);
        ////    IRestResponse response = client.Execute(request);

        //    var config = Externals.Resolve<IConfig>();
        //    var address = await config.GetAuthUrlAsync();
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri(address);
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
        //    return client;
        //}
    }
}
