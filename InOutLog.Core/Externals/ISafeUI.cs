using System;

namespace InOutLog.Core
{
    public interface ISafeUI
    {
        void Invoke(Action action);
    }
}
