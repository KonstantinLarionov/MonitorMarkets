using MonitorMarkets.Application.Objects.DataBase;

namespace MonitorMarkets.Databases.Entities
{
    public class PositionsEntities
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public OrderActionEnum StatusPosition { get; set; }
    }
}