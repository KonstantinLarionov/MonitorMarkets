using System.Runtime.CompilerServices;
using BybitMapper.UsdcPerpetual.RestV2.Data.Enums;
using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class PlaceOrderResponse
    {
        public PlaceOrderResponse(string id, decimal price, decimal amount, OrderActionEnum direction,
            OrderMarkerEnum marker)
        {
            Id = id;
            Price = price;
            Amount = amount;
            Direction = direction;
            Marker = marker;
        }
        public string Id { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public OrderActionEnum Direction { get; set; }
        public OrderMarkerEnum Marker { get; set; }
    }
}