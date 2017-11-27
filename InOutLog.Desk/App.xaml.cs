using InOutLog.Core;
using System.Windows;
using System;
using System.Windows.Interop;
using System.Windows.Navigation;

namespace InOutLog.Desk
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Externals.Register<IConfig>(() => new Config(), true);
            Externals.Register<IAuthManager>(() => new AuthManager(), true);
            Externals.Register<ISafeUI>(() => new SafeUI());
            Externals.Register<IDialog>(() => new Dialog());

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Environment.Exit(0);
        }

        private class WindowWrapper : System.Windows.Forms.IWin32Window
        {
            public WindowWrapper(IntPtr handle)
            {
                _hwnd = handle;
            }

            public WindowWrapper(Window window)
            {
                _hwnd = new WindowInteropHelper(window).Handle;
            }

            public IntPtr Handle
            {
                get { return _hwnd; }
            }

            private IntPtr _hwnd;
        }
    }
}
