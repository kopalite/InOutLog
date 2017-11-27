using System;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public interface IAuthManager
    {
        AuthData AuthData { get; }

        Task StartSignInAsync(params object[] args);

        Task AfterSignInAsync(params object[] args);
    }
}