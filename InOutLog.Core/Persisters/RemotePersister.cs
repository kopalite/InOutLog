﻿using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public class RemotePersister : ILogPersister
    {
        private readonly IAuthManager _authManager;
        private readonly ILogPersister _fallbackPersister;

        public bool IsLocalMode { get; private set; }

        public RemotePersister(IAuthManager authManager)
        {
            _authManager = authManager;
            _fallbackPersister = new LocalPersister(_authManager);
            IsLocalMode = false;
        }

        public async Task<Entry> RestoreAsync()
        {
            if (IsLocalMode)
            {
                return await _fallbackPersister.RestoreAsync();
            }

            try
            {
                var client = GetClient();
                var username = _authManager.AuthData.Username;
                var entryDate = Entry.GetEntryDate();
                var url = string.Format("/api/entries/find/{0}/{1}", username, entryDate);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var serialized = await response.Content.ReadAsStringAsync();
                    var entries = JsonConvert.DeserializeObject<Entry[]>(serialized);
                    return entries.OrderBy(x => x.EntryDate).LastOrDefault();
                }
                else
                {
                    IsLocalMode = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                IsLocalMode = true;
            }

            if (IsLocalMode)
            {
                return await _fallbackPersister.RestoreAsync();
            }

            return await Task.FromResult<Entry>(null);
        }

        public async Task<string> PersistAsync(Entry entry)
        {
            if (entry == null)
            {
                return null;
            }

            if (IsLocalMode)
            {
                return await _fallbackPersister.PersistAsync(entry);
            }

            try
            {
                var entryJson = JsonConvert.SerializeObject(entry);
                var client = GetClient();
                var content = new StringContent(entryJson, Encoding.UTF8, "application/json");
                var response = await client.PutAsync("/api/entries", content);
                if (response.IsSuccessStatusCode)
                {
                    return entryJson;
                }
                else
                {
                    IsLocalMode = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                IsLocalMode = true;
            }

            if (IsLocalMode)
            {
                return await _fallbackPersister.PersistAsync(entry);
            };

            return null;
        }

        public async Task RemoveAsync()
        {
            if (IsLocalMode)
            {
                await _fallbackPersister.RemoveAsync();
                return;
            }

            try
            {
                var client = GetClient();
                var username = _authManager.AuthData.Username;
                var entryDate = Entry.GetEntryDate();
                var url = string.Format("/api/entries/{0}/{1}", username, entryDate);
                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    IsLocalMode = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                IsLocalMode = true;
            }

            if (IsLocalMode)
            {
                await _fallbackPersister.RemoveAsync();
            };
        }

        private HttpClient GetClient()
        {
            var address = Settings.ApiUrl;
            var client = new HttpClient { BaseAddress = new Uri(address) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("authorization", string.Format("Bearer {0}", _authManager.AuthData.Token));
            return client;
        }
    }
}
