using InOutLog.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOutLog.Desk
{
    internal class Config : IConfig
    {
        public async Task<string> GetAuthUrlAsync()
        {
            var result = ConfigurationManager.AppSettings["authUrl"];
            return await Task.FromResult(result);
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
    }
}
