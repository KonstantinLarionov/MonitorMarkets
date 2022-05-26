using System.Runtime.Serialization;
using MonitorMarkets.Application.Objects.Data.Attributes;

namespace MonitorMarkets.Application.Objects.Data.Enums
{
    public enum MarketsEnum
    {
        [EnumMarkets("Binance", null, null,null, false)]
        Binance,
        [EnumMarkets("Bybit", null, null,null, false)]
        Bybit,
        [EnumMarkets("Bitget", null, null,null, false)]
        Bitget
    }
}