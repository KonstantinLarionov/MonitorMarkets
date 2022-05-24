using BitgetMapper.Futures.RestAPI.Responses.Account;

namespace MonitorMarkets.Application.Objects.Data
{
    public class CancelOrderData
    {
        public CancelOrderData(string orderId, string clientOid)
        {
            OrderId = orderId;
            ClientOid = clientOid;
        }
        public string OrderId { get; set; }
        public string ClientOid { get; set; }
    }
}