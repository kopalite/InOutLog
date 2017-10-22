using System;
using InOutLog.Core;
using System.Windows;
using System.Windows.Threading;

namespace InOutLog.Desk
{
    public class SafeUI : ISafeUI
    {
        public void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => action()), DispatcherPriority.ContextIdle);
        }
    }
}
