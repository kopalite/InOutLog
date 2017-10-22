using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InOutLog.Core.Extensions
{
    public static class SyncExtensions
    {
        public static void SafeInvoke(this SynchronizationContext context, Action action)
        {
            if (SynchronizationContext.Current == context)
            {
                action();
            }
            else
            {
                context.Send(x => action(), null);
            }
        }
    }
}
