using Android.App;
using Android.Widget;
using Android.OS;
using InOutLog.Core;
using GalaSoft.MvvmLight.Helpers;
using System;
using Android.Content.PM;
using Android.Content;

namespace InOutLog.Droid
{
    [Activity(Label = "In/Out Log", MainLauncher = true, Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleTask)]

    [IntentFilter(new[] { Intent.ActionView }, 
                  Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
                  DataScheme = "inoutlog.droid", 
                  DataHost = "inoutlog.auth0.com", 
                  DataPathPrefix = "/android/inoutlog.droid/callback")]

    public partial class MainActivity : Activity
    {
        private MainViewModel _mainViewModel;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _mainViewModel = new MainViewModel();
            await _mainViewModel.AuthManager.StartSignInAsync();
        }

        protected override async void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            if (_mainViewModel.AuthManager.AuthData == null)
            {
                await _mainViewModel.AuthManager.AfterSignInAsync(intent);
                await _mainViewModel.Watcher.StartupAsync();

                SetContentViewBindings();
            }
        }

        private void SetContentViewBindings()
        {
            SetContentView(Resource.Layout.Main);

            //commands setting

            CheckInButton.SetCommand(_mainViewModel.Watcher.CheckInCommand);
            CheckOutButton.SetCommand(_mainViewModel.Watcher.CheckOutCommand);           
            BreakInButton.SetCommand(_mainViewModel.Watcher.BreakInCommand);
            BreakOutButton.SetCommand(_mainViewModel.Watcher.BreakOutCommand);
            ResetButton.SetCommand(_mainViewModel.Watcher.ResetCommand);

            //labels setting

            Func<DateTime?, string> displayClock = x => (string)(new ClockConverter().Convert(x));

            this.SetBinding(() => _mainViewModel.Watcher.State.Data.StartedAt, () => StartedAtLabel.Text)
                   .ConvertSourceToTarget(displayClock);
            
            this.SetBinding(() => _mainViewModel.Watcher.State.Data.StoppedAt, () => StoppedAtLabel.Text)
                .ConvertSourceToTarget(displayClock);

            this.SetBinding(() => _mainViewModel.Watcher.State.Data.BreakStartedAt, () => BreakStartedAtLabel.Text)
                   .ConvertSourceToTarget(displayClock);

            this.SetBinding(() => _mainViewModel.Watcher.State.Data.BreakStoppedAt, () => BreakStoppedAtLabel.Text)
                .ConvertSourceToTarget(displayClock);

            Func<TimeSpan, string> displaySpan = x => (string)(new TimeSpanConverter().Convert(x));

            this.SetBinding(() => _mainViewModel.Watcher.State.Data.CheckInTime, () => CheckInTimeLabel.Text)
                .ConvertSourceToTarget(displaySpan);

            this.SetBinding(() => _mainViewModel.Watcher.State.Data.BreaksTotal, () => BreakInTimeLabel.Text)
                .ConvertSourceToTarget(displaySpan);

            this.SetBinding(() => _mainViewModel.Watcher.State.Data.WorkingTime, () => WorkingTimeLabel.Text)
                .ConvertSourceToTarget(displaySpan);
        }
    }
}

