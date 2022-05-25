using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Market;
using System;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class KlineResponse
    {
        public KlineResponse(DateTime startTime, double openPrice, double closePrice, double highPrice, double lowPrice, decimal volume)
        {
            StartTime = startTime;
            OpenPrice = openPrice;
            ClosePrice = closePrice;
            HighPrice = highPrice;
            LowPrice = lowPrice;
            Volume = volume;
        }
        public DateTime StartTime { get; }
        public double OpenPrice { get; }
        public double ClosePrice { get; }
        public double HighPrice { get; }
        public double LowPrice { get; }
        public decimal Volume { get; }

    }
}