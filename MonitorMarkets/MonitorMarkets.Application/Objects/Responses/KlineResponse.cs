using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Market;
using System;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class KlineResponse
    {
        public KlineResponse(long startTime, decimal openPrice, decimal closePrice, decimal highPrice, decimal lowPrice, decimal volume)
        {
            StartTime = startTime;
            OpenPrice = openPrice;
            ClosePrice = closePrice;
            HighPrice = highPrice;
            LowPrice = lowPrice;
            Volume = volume;
        }
        public long StartTime { get; }
        public decimal OpenPrice { get; }
        public decimal ClosePrice { get; }
        public decimal HighPrice { get; }
        public decimal LowPrice { get; }
        public decimal Volume { get; }

    }
}