using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Account.Positions;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class QueryMyPositionsResponse
    {
        public QueryMyPositionsResponse(string symbol, double price, decimal amount)
        {
            Symbol = symbol;
            Price = price;
            Amount = amount;

        }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public decimal Amount { get; set; }
    }
}