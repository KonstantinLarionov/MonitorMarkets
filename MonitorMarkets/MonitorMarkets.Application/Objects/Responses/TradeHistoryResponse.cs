using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class TradeHistoryResponse
    {
        public TradeHistoryResponse(string symbol, long moment, string id, string orderId, decimal? price, decimal amount,
            decimal fee, string feeCurrency)
        {
            Symbol = symbol;
            Moment = moment;
            Id = id;
            OrderId = orderId;
            Price = price;
            Amount = amount;
            Fee = fee;
            FeeCurrency = feeCurrency;
        }
        
        public string Symbol { get; set; }
        public long Moment { get; set; }
        public string Id { get; set; }
        public string OrderId { get; set; }
        public decimal? Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string FeeCurrency { get; set; }


    }
}