using System;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public interface IConfig
    {
        Task<string> GetAuthDomainAsync();

        Task<string> GetAuthClientIdAsync();

        Task<string> GetApiUrlAsync();

        Task<TimeSpan> GetRefreshIntervalAsync();
    }
}
