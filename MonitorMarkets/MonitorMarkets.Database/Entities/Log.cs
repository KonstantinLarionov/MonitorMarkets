using System;

namespace MonitorMarkets.Database.Entities
{
    public class Log
    {
        public string Id { get; set; }
        public string TypeError { get; set; }
        public string MsgError { get; set; }
        public DateTime Time { get; set; }
    }
}