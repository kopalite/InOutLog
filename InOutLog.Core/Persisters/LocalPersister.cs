﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using PCLStorage;

namespace InOutLog.Core
{
    public class LocalPersister : ILogPersister
    {
        private const string FileName = "inoutlog.json";

        public bool IsLocalMode { get { return true; } }

        private class Log
        {
            public List<Entry> Entries { get; set; }

            public Log()
            {
                Entries = new List<Entry>();
            }
        }
        
        public async Task<Entry> RestoreAsync()
        {
            var folder = await GetLocalFolder();
            var fileExists = await folder.CheckExistsAsync(FileName);
            if (fileExists == ExistenceCheckResult.NotFound)
            {
                return null;
            }
            var file = await folder.CreateFileAsync(FileName, CreationCollisionOption.OpenIfExists);
            var content = await file.ReadAllTextAsync();
            var log = JsonConvert.DeserializeObject<Log>(content);
            var username = await Config.GetUsernameAsync();
            return log.Entries.FirstOrDefault(x => x.Username == username && x.EntryDate == DateTime.Today);

        }

        public async Task<string> PersistAsync(Entry entry)
        {
            if (entry == null)
            {
                return null;
            }

            var log = new Log();

            var folder = await GetLocalFolder();
            var fileExists = await folder.CheckExistsAsync(FileName);
            if (fileExists == ExistenceCheckResult.FileExists)
            {
                var existingFile = await folder.CreateFileAsync(FileName, CreationCollisionOption.OpenIfExists);
                var content = await existingFile.ReadAllTextAsync();
                log = JsonConvert.DeserializeObject<Log>(content);
            }

            var username = await Config.GetUsernameAsync();
            var existingEntry = log.Entries.FirstOrDefault(x => x.Username == username && x.EntryDate == DateTime.Today);
            if (existingEntry != null)
            {
                log.Entries.Remove(existingEntry);
            }

            log.Entries.Add(entry);
            var entryJson = JsonConvert.SerializeObject(log);
            var file = await folder.CreateFileAsync(FileName, CreationCollisionOption.OpenIfExists);
            await file.WriteAllTextAsync(entryJson);
            return entryJson;

        }

        private async Task<IFolder> GetLocalFolder()
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            return await rootFolder.CreateFolderAsync("localstore", CreationCollisionOption.OpenIfExists);
        }
    }
}