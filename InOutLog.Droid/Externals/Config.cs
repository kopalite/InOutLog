using Android.App;
using Android.Content;
using InOutLog.Core;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InOutLog.Droid
{
    internal class Config : IConfig
    {
        private readonly Context _context;

        public Config(Context context)
        {
            _context = context;
        }

        public async Task<string> GetAuthDomainAsync()
        {
            await EnsurePreferenceValueAsync("authDomain", "inoutlog.auth0.com");
            return await GetPreferenceValueAsync("authDomain");
        }

        public async Task<string> GetAuthClientIdAsync()
        {
            await EnsurePreferenceValueAsync("authClientId", "0wHjlNC-Csn7M5vdf-j4djW_0r-V9aJM");
            return await GetPreferenceValueAsync("authClientId");
        }

        public async Task<string> GetApiUrlAsync()
        {
            await EnsurePreferenceValueAsync("apiUrl", "http://inoutlog.openode.io");
            return await GetPreferenceValueAsync("apiUrl");
        }

        public async Task<TimeSpan> GetRefreshIntervalAsync()
        {
            await EnsurePreferenceValueAsync("refreshInterval", "30");
            var seconds = int.Parse(await GetPreferenceValueAsync("refreshInterval"));
            return TimeSpan.FromSeconds(seconds);
        }

        private async Task EnsurePreferenceValueAsync(string key, string value)
        {
            var preferences = _context.GetSharedPreferences(_context.PackageName, FileCreationMode.Private);
            var existingValue = preferences.GetString(key, null);
            if (existingValue == null)
            {
                var editor = preferences.Edit();
                editor.PutString(key, value);
                editor.Commit();
            }
            await Task.FromResult<object>(null);
        }

        private async Task<string> GetPreferenceValueAsync(string key)
        {
            var preferences = _context.GetSharedPreferences(_context.PackageName, FileCreationMode.Private);
            var value = preferences.GetString(key, null);
            return await Task.FromResult(value);
        }
    }
}
