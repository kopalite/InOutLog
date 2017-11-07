using System.Threading.Tasks;

namespace InOutLog.Core
{
    public interface IAuthManager
    {
        Task<AuthData> GetAuthDataAsync();

        Task<AuthData> SignInUserAsync();
    }
}