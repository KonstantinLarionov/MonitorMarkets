using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace MonitorMarkets.Application.Objects.DataBase
{
    public class LogInfo
    {
        public string TypeError { get; set; } 
        public string MsgError { get; set; }
        public DateTime Time { get; set; }
        
    }
}