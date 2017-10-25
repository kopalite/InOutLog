using Android.App;
using Newtonsoft.Json;
using PCLStorage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public class Config
    {
        private static ConfigSettings _settings;

        public static async Task<string> GetUsernameAsync()
        {
            await InitAsync();
            return _settings.Username;
        }

        public static async Task<string> GetApiUrlAsync()
        {
            await InitAsync();
            return _settings.ApiUrl;
        }

        public static async Task<TimeSpan> GetRefreshIntervalAsync()
        {
            await InitAsync();
            return TimeSpan.FromSeconds(_settings.RefreshInterval);
        }

        private static async Task InitAsync()
        {
            if (_settings != null)
            {
                return;
            }

            _settings = new ConfigSettings
            {
                ApiUrl = "http://192.168.0.102:3000/",
                RefreshInterval = 5,
                Username = "ivan.kopcanski"
            };

            await Task.FromResult<object>(null);

            //var configFile = await FileSystem.Current.GetFileFromPathAsync(@"config.json");
            //var content = await configFile.ReadAllTextAsync();
            //var settings = JsonConvert.DeserializeObject<ConfigSettings>(content);
            //_settings = settings;
            
        }
    }

    internal class ConfigSettings
    {
        public string Username { get; set; }

        public string ApiUrl { get; set; }

        public int RefreshInterval { get; set; }
    }
}
