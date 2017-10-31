namespace InOutLog.Core
{
    public class AuthData
    {
        
        public string Username { get; private set; }
        
        public string Error { get; private set; }

        public AuthData(string username)
        {
            Username = username;
        }

        public AuthData(string username, string error)
        {
            Username = username;
            Error = error;
        }
    }
}