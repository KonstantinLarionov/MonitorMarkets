using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Market;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class OrderBookResponse
    {
        public OrderBookResponse(decimal price, decimal amount)
        {
            Price = price;
            Amount = amount;
        }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}