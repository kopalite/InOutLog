using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace InOutLog.Core
{
    public static class Settings
    {
        #region [ Keys & Defaults ]

        private const string AuthDomain_Key = "authDomain";
        private static readonly string AuthDomain_Default = "inoutlog.auth0.com";

        private const string AuthClientId_Key = "authClientId";
        private static readonly string AuthClientId_Default = "0wHjlNC-Csn7M5vdf-j4djW_0r-V9aJM";

        private const string AuthAudience_Key = "authAudience";
        private static readonly string AuthAudience_Default = "https://inoutlog.auth0.com/api/v2/";

        private const string ApiUrl_Key = "apiUrl";
        private static readonly string ApiUrl_Default = "http://inoutlog.openode.io";

        private const string RefreshInterval_Key = "refreshInterval";
        private static readonly string RefreshInterval_Default = "30";

        #endregion

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string AuthDomain
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthDomain_Key, AuthDomain_Default);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthDomain_Key, value);
            }
        }

        public static string AuthClientId
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthClientId_Key, AuthClientId_Default);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthClientId_Key, value);
            }
        }

        public static string AuthAudience
        {
            get
            {
                return AppSettings.GetValueOrDefault(AuthAudience_Key, AuthAudience_Default);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AuthAudience_Key, value);
            }
        }

        public static string ApiUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(ApiUrl_Key, ApiUrl_Default);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ApiUrl_Key, value);
            }
        }

        public static TimeSpan RefreshInterval
        {
            get
            {
                var seconds = int.Parse(AppSettings.GetValueOrDefault(RefreshInterval_Key, RefreshInterval_Default));
                return TimeSpan.FromSeconds(seconds);
            }
            set
            {
                int seconds = 0;
                if (value != null)
                {
                    seconds = value.Seconds;
                }
                AppSettings.AddOrUpdateValue(RefreshInterval_Key, seconds.ToString());
            }
        }

    }
}
