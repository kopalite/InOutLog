using System;
using Android.App;
using Android.Content;
using InOutLog.Core;
using System.Threading.Tasks;
using Plugin.CurrentActivity;

namespace InOutLog.Droid
{
    internal class Dialog : IDialog
    {
        public async Task AlertAsync(string title, string message)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            var activity = CrossCurrentActivity.Current.Activity;
            var alert = new AlertDialog.Builder(activity);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (obj, args) => { tcs.SetResult(null); });
            alert.Create().Show();

            await tcs.Task;
        }

        public async Task OptionAsync(string title, string message, Action yesAction, Action noAction)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            var activity = CrossCurrentActivity.Current.Activity;
            var alert = new AlertDialog.Builder(activity);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("Yes", (obj, args) => { yesAction(); tcs.SetResult(null); });
            alert.SetNegativeButton("No", (obj, args) => { noAction(); tcs.SetResult(null); });
            alert.Create().Show();

            await tcs.Task;
        }
    }
}