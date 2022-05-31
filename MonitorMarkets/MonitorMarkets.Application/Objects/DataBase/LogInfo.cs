using System;

namespace MonitorMarkets.Application.Objects.DataBase
{
    public class LogInfo
    {
        public string Id { get; set; }
        public string TypeError { get; set; }
        public string MsgError { get; set; }
        public DateTime Time { get; set; }
        
    }
}