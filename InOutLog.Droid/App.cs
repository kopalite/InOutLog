using System;
using Android.App;
using Android.Runtime;
using InOutLog.Core;

namespace InOutLog.Droid
{
    [Application]
    public class App : Application
    {
        public App(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
            //Externals.Register<IConfig>(() => new Config(this), true);
            //Externals.Register<IAuthManager>(() => new AuthManager(), true);
            //Externals.Register<ISafeUI>(() => new SafeUI());
            //Externals.Register<IDialog>(() => new Dialog(this));

            //Externals.Lock();
        }
    }
}