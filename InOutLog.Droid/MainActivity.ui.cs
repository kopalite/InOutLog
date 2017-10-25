using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Views;

namespace InOutLog.Droid
{
    public partial class MainActivity
    {
        private Button _checkInButton;

        public Button CheckInButton
        {
            get
            {
                return _checkInButton 
                    ?? (_checkInButton = FindViewById<Button>(Resource.Id.CheckInButton));
            }
        }


        private Button _checkOutButton;

        public Button CheckOutButton
        {
            get
            {
                return _checkOutButton
                       ?? (_checkOutButton = FindViewById<Button>(Resource.Id.CheckOutButton));
            }
        }


        private Button _breakInButton;

        public Button BreakInButton
        {
            get
            {
                return _breakInButton
                       ?? (_breakInButton = FindViewById<Button>(Resource.Id.BreakInButton));
            }
        }


        private Button _breakOutButton;

        public Button BreakOutButton
        {
            get
            {
                return _breakOutButton
                       ?? (_breakOutButton = FindViewById<Button>(Resource.Id.BreakOutButton));
            }
        }


        private Button _resetButton;

        public Button ResetButton
        {
            get
            {
                return _resetButton
                       ?? (_resetButton = FindViewById<Button>(Resource.Id.ResetButton));
            }
        }


        private TextView _startedAtLabel;

        public TextView StartedAtLabel
        {
            get
            {
                return _startedAtLabel
                       ?? (_startedAtLabel = FindViewById<TextView>(Resource.Id.StartedAtLabel));
            }
        }


        private TextView _stoppedAtLabel;

        public TextView StoppedAtLabel
        {
            get
            {
                return _stoppedAtLabel
                       ?? (_stoppedAtLabel = FindViewById<TextView>(Resource.Id.StoppedAtLabel));
            }
        }


        private TextView _breakStartedAtLabel;

        public TextView BreakStartedAtLabel
        {
            get
            {
                return _breakStartedAtLabel
                       ?? (_breakStartedAtLabel = FindViewById<TextView>(Resource.Id.BreakStartedAtLabel));
            }
        }


        private TextView _breakStoppedAtLabel;

        public TextView BreakStoppedAtLabel
        {
            get
            {
                return _breakStoppedAtLabel
                       ?? (_breakStoppedAtLabel = FindViewById<TextView>(Resource.Id.BreakStoppedAtLabel));
            }
        }


        private TextView _checkInTimeLabel;

        public TextView CheckInTimeLabel
        {
            get
            {
                return _checkInTimeLabel
                       ?? (_checkInTimeLabel = FindViewById<TextView>(Resource.Id.CheckInTimeLabel));
            }
        }

        private TextView _breakInTimeLabel;

        public TextView BreakInTimeLabel
        {
            get
            {
                return _breakInTimeLabel
                       ?? (_breakInTimeLabel = FindViewById<TextView>(Resource.Id.BreakInTimeLabel));
            }
        }


        private TextView _workingTimeLabel;

        public TextView WorkingTimeLabel
        {
            get
            {
                return _workingTimeLabel
                       ?? (_workingTimeLabel = FindViewById<TextView>(Resource.Id.WorkingTimeLabel));
            }
        }
    }
}