using System;
using Android.App;
using Android.Runtime;

namespace InOutLog.Droid
{
    [Application]
    public class App : Application//, IActivityLifecycleCallbacks
    {
        public Activity CurrentActivity { get; private set; }

        public App(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
            //Externals.Register<IConfig>(() => new Config(this), true);
            //Externals.Register<IAuthManager>(() => new AuthManager(), true);
            //Externals.Register<ISafeUI>(() => new SafeUI());
            //Externals.Register<IDialog>(() => new Dialog(this));
        }

        //public override void OnCreate()
        //{
        //    base.OnCreate();

        //    RegisterActivityLifecycleCallbacks(this);

        //    //Externals.Register<IConfig>(() => new Config(this), true);
        //    //Externals.Register<IAuthManager>(() => new AuthManager(), true);
        //    //Externals.Register<ISafeUI>(() => new SafeUI());
        //    //Externals.Register<IDialog>(() => new Dialog(this));
        //}

        //public override void OnTerminate()
        //{
        //    base.OnTerminate();
        //    UnregisterActivityLifecycleCallbacks(this);
        //}

        //public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        //{
        //    CurrentActivity = activity;
        //}

        //public void OnActivityDestroyed(Activity activity)
        //{
        //}

        //public void OnActivityPaused(Activity activity)
        //{
        //}

        //public void OnActivityResumed(Activity activity)
        //{
        //    CurrentActivity = activity;
        //}

        //public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        //{
        //}

        //public void OnActivityStarted(Activity activity)
        //{
        //    CurrentActivity = activity;
        //}

        //public void OnActivityStopped(Activity activity)
        //{
        //}
    }
}