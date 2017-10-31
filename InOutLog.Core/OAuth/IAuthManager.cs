using System.Threading.Tasks;

namespace InOutLog.Core
{
    public interface IAuthManager
    {
        Task<AuthData> GetAuthDataAsync();

        Task<AuthData> SignUpUserAsync(string username, string password);

        Task<AuthData> SignInUserAsync(string username, string password);

        Task<AuthData> SignInUserAsync();
    }
}