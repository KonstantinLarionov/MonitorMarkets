using System.Runtime.Serialization;
using MonitorMarkets.Application.Objects.Data.Attributes;

namespace MonitorMarkets.Application.Objects.Data.Enums
{
    public enum MarketsEnum
    {
        [EnumMarkets("Binance", null, null,null, false)]
        Binance = 0,
        [EnumMarkets("Bybit", null, null,null, false)]
        Bybit = 1,
        [EnumMarkets("Bitget", null, null,null, false)]
        Bitget= 2
    }
}