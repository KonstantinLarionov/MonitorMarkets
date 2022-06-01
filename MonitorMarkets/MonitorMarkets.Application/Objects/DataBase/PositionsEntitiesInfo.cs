namespace MonitorMarkets.Application.Objects.DataBase
{
    public class PositionsEntitiesInfo
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public OrderActionEnum StatusPosition { get; set; }
    }
}