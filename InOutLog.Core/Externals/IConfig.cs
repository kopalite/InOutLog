using System;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public interface IConfig
    {
        Task<string> GetUsernameAsync();

        Task<string> GetApiUrlAsync();

        Task<TimeSpan> GetRefreshIntervalAsync();
    }
}
