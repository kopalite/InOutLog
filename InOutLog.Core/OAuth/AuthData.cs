using System;

namespace InOutLog.Core
{
    public class AuthData
    {
        public string Username { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string Token { get; private set; }

        public DateTime ExpiresOn { get; private set; }

        public string Error { get; private set; }

        public AuthData(string username, bool isAuthenticated, string token, DateTime expiresOn, string error)
        {
            Username = username;
            IsAuthenticated = isAuthenticated;
            Token = token;
            ExpiresOn = expiresOn;
            Error = error;
        }
    }
}