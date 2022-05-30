namespace MonitorMarkets.Database.Entities
{
    public class PositionsEntities
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public OrderActionEnum StatusPosition { get; set; }
    }
}