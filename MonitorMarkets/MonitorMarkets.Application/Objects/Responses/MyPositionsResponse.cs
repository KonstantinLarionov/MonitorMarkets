using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Account.Positions;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class MyPositionsResponse
    {
        public MyPositionsResponse(string symbol, decimal price, decimal amount)
        {
            Symbol = symbol;
            Price = price;
            Amount = amount;

        }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}