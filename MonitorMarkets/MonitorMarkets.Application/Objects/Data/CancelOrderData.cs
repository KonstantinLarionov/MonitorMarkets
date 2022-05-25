using BitgetMapper.Futures.RestAPI.Responses.Account;

namespace MonitorMarkets.Application.Objects.Data
{
    public class CancelOrderData
    {
        public CancelOrderData(string orderId)
        {
            OrderId = orderId;
        }
        public string OrderId { get; set; }
    }
}