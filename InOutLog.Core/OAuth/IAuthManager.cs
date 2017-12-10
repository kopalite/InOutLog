using System;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public interface IAuthManager
    {
        AuthData AuthData { get; }

        Task StartSignInAsync();

        Task AfterSignInAsync(params object[] args);
    }
}