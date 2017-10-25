using System;
using InOutLog.Core;

namespace InOutLog.Droid
{
    internal class SafeUI : ISafeUI
    {
        public void Invoke(Action action)
        {
            action();
        }
    }
}