namespace MonitorMarkets.Application.Objects.Responses
{
    public class PlaceOrderResponse
    {
        public PlaceOrderResponse(string orderId, string orderLinkId, string symbol, string orderPrice, string orderQty,
            string orderType, string side)
        {
            Symbol = symbol;
            OrderQty = orderQty;
            OrderType = orderType;
            Side = side;
        }
        
        public string OrderId { get; set; }
        public string OrderLinkId { get; set; }
        public string Symbol { get; set; }
        public string OrderPrice { get; set; }
        public string OrderQty { get; set; }
        public string OrderType { get; set; }
        public string Side { get; set; }

    }
}