using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public class Entry
    {
        public string SessionId { get; set; }
        
        public string EntryDate { get; set; }
        
        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value.ToUtc(); }
        }

        public string Username { get; set; }

        public int StateId { get; set; }

        public WatcherData Data { get; set; }

        internal static async Task<Entry> CreateAsync(string username, int stateId, WatcherData data)
        {
            return new Entry
            {
                SessionId = Session.SessionId,
                Timestamp = DateTime.UtcNow,
                EntryDate = GetEntryDate(),
                Username = username,
                StateId = stateId,
                Data = data 
            };
        }

        private const string ShortDateFormat = "yyyy-MM-dd";

        internal static string GetEntryDate()
        {
            return DateTime.UtcNow.ToString(ShortDateFormat);
        }

        internal static string GetDisplayDate()
        {
            return DateTime.Now.ToString(ShortDateFormat);
        }
    }
}
