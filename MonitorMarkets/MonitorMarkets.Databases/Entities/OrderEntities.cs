using MonitorMarkets.Application.Objects.DataBase;

namespace MonitorMarkets.Databases.Entities
{
        public class OrdersEntities
        {
                public string Id { get; set; }
                public decimal Price { get; set; }
                public decimal Amount { get; set; }
                public OrderActionEnum Direction { get; set; }
                public StatusOrderEnum StatusOrder { get; set; }
        }

}