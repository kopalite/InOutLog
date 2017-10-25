using System;
using Android.App;
using Android.Content;
using InOutLog.Core;
using System.Threading.Tasks;

namespace InOutLog.Droid
{
    internal class Dialog : IDialog
    {
        private readonly Context _context;

        public Dialog(Context context)
        {
            _context = context;
        }

        public void Alert(string title, string message)
        {
            var alert = new AlertDialog.Builder(_context);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (obj, args) => {});
            alert.Create().Show();
        }

        public async Task OptionAsync(string title, string message, Action yesAction, Action noAction)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            var alert = new AlertDialog.Builder(_context);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("Yes", (obj, args) => { yesAction(); tcs.SetResult(null); });
            alert.SetNegativeButton("No", (obj, args) => { noAction(); tcs.SetResult(null); });
            alert.Create().Show();

            await tcs.Task;
        }
    }
}