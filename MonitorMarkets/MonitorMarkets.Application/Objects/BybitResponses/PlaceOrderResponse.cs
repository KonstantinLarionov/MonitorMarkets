using BybitMapper.UsdcPerpetual.RestV2.Data.Enums;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class PlaceOrderResponse
    {
        public PlaceOrderResponse(string orderId, string orderLinkId, string symbol, decimal orderPrice, decimal orderQty,
            OrderType orderType, string side)
        {
            OrderId = orderId;
            OrderLinkId = orderLinkId;
            Symbol = symbol;
            OrderPrice = orderPrice;
            OrderQty = orderQty;
            OrderType = orderType;
            Side = side;
        }
        
        public string OrderId { get; set; }
        public string OrderLinkId { get; set; }
        public string Symbol { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal OrderQty { get; set; }
        public OrderType OrderType { get; set; }
        public string Side { get; set; }

    }
}