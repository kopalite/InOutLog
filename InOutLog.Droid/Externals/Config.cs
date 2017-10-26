using InOutLog.Core;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOutLog.Droid
{
    internal class Config : IConfig
    {
        public Config()
        {
            ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);
        }

        public async Task<string> GetApiUrlAsync()
        {
            var result = ConfigurationManager.AppSettings["apiUrl"];
            return await Task.FromResult(result);
        }

        public async Task<TimeSpan> GetRefreshIntervalAsync()
        {
            var result = int.Parse(ConfigurationManager.AppSettings["refreshInterval"]);
            return await Task.FromResult(TimeSpan.FromSeconds(result));
        }

        public async Task<string> GetUsernameAsync()
        {
            var result = ConfigurationManager.AppSettings["username"];
            return await Task.FromResult(result);
        }
    }
}
