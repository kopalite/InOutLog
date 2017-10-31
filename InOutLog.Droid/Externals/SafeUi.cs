using System;
using InOutLog.Core;
using Android.App;

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