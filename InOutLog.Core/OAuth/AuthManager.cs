using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public class AuthManager : IAuthManager
    {
        public async Task<AuthData> SignUpUserAsync(string username, string password)
        {
            await Task.Delay(5000);
            return new AuthData(username);
            
        }

        public async Task<AuthData> SignInUserAsync(string username, string password)
        {
            await Task.Delay(5000);
            return new AuthData(username);
        }

        public async Task<AuthData> SignInUserAsync()
        {
            await Task.Delay(5000);
            return new AuthData("ivan.kopcanski");
        }


    }
}
