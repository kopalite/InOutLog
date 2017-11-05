using InOutLog.Core;
using System.Windows;
using System;

namespace InOutLog.Desk
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Externals.Register<IAuthManager>(() => new AuthManager());
            Externals.Register<IConfig>(() => new Config());
            Externals.Register<ISafeUI>(() => new SafeUI());
            Externals.Register<IDialog>(() => new Dialog());

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
