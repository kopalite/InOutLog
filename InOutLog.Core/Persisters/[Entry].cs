using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public class Entry
    {
        public const string TodayDateFormat = "yyyy-MM-dd";

        public string SessionId { get; set; }

        [JsonConverter(typeof(TodayDateTimeConverter))]
        public DateTime EntryDate { get; set; }

        public string Username { get; set; }

        public int StateId { get; set; }

        public WatcherData Data { get; set; }

        internal static async Task<Entry> CreateAsync(int stateId, WatcherData data)
        {
            return new Entry
            {
                SessionId = Session.SessionId,
                EntryDate = DateTime.Today,
                Username = await Config.GetUsernameAsync(),
                StateId = stateId,
                Data = data 
            };
        }
    }

    public class TodayDateTimeConverter : IsoDateTimeConverter
    {
        public TodayDateTimeConverter()
        {
            DateTimeFormat = Entry.TodayDateFormat;
        }
    }
}
