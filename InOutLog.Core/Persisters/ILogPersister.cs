using System.Collections.Generic;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public interface ILogPersister
    {
        bool IsLocalMode { get; }

        Task<Entry> RestoreAsync();

        Task<string> PersistAsync(Entry entry);

        Task RemoveAsync();
    }
}
