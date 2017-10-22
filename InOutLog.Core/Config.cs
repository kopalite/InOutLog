using Newtonsoft.Json;
using PCLStorage;
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

        public static async Task<int> GetRefreshIntervalAsync()
        {
            await InitAsync();
            return _settings.RefreshInterval;
        }

        private static async Task InitAsync()
        {
            if (_settings != null)
            {
                return;
            }

            var configFile = await FileSystem.Current.GetFileFromPathAsync("config.json");
            var content = await configFile.ReadAllTextAsync();
            var settings = JsonConvert.DeserializeObject<ConfigSettings>(content);
            _settings = settings;
            
        }
    }

    internal class ConfigSettings
    {
        public string Username { get; set; }

        public string ApiUrl { get; set; }

        public int RefreshInterval { get; set; }
    }
}
