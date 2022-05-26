using Newtonsoft.Json.Serialization;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class CancelOrderResponse
    {
        public CancelOrderResponse(string orderId)
        {
            OrderId = orderId;
        }
        public string OrderId { get; set; }
    }
}