using System;
using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class OnTickResponse
    {
        public OnTickResponse(long dateTime, decimal price, decimal amount, OrderActionEnum direction)
        {
            DateTime = dateTime;
            Price = price;
            Amount = amount;
            Direction = direction;
        }
        public long DateTime { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public OrderActionEnum Direction { get; set; }
    }
}