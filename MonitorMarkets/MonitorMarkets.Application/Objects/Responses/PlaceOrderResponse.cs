using BybitMapper.UsdcPerpetual.RestV2.Data.Enums;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class PlaceOrderResponse
    {
        public PlaceOrderResponse(string orderId)
        {
            OrderId = orderId;
        }
        
        public string OrderId { get; set; }
    }
}