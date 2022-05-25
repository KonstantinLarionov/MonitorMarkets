using System;
using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order;
using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class OrderHistoryResponse
    {
        public OrderHistoryResponse(string symbol, string id, OrderActionEnum direction, double price, decimal originalAmount
        , decimal remainAmount, long moment, OrderTypeEnum type, TriggerTypeEnum triggerType, OrderStateEnum state)
        {
            Symbol = symbol;
            Id = id;
            Direction = direction;
            Price = price;
            OriginalAmount = originalAmount;
            RemainAmount = remainAmount;
            Moment = moment;
            Type = type;
            TriggerType = triggerType;
            State = state;
        }
        public string Symbol { get; set; }
        public string Id { get; set; }
        public OrderActionEnum Direction { get; set; }
        public double Price { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal RemainAmount { get; set; }
        public long Moment { get; set; }
        public OrderTypeEnum Type { get; set; }
        public TriggerTypeEnum TriggerType { get; set; }
        public OrderStateEnum State { get; set; }

    }
}