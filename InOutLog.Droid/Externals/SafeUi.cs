using System;
using InOutLog.Core;
using Android.App;
using Plugin.CurrentActivity;
using System.Diagnostics;

namespace InOutLog.Droid
{
    internal class SafeUI : ISafeUI
    {
        public void Invoke(Action action)
        {
            var activity = CrossCurrentActivity.Current.Activity;
            activity.RunOnUiThread(action);
        }
    }
}