namespace MonitorMarkets.Application.Objects.DataBase
{
    public class OrdersEntitiesInfo
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public OrderActionEnum Direction { get; set; }
        public StatusOrderEnum StatusOrder { get; set; }
    }
    
    public enum OrderActionEnum 
    {
        Unknown = 0,
        Buy = 1,
        Sell = 2,
    }

    public enum StatusOrderEnum
    {
        Unknown = 0,
        Accepted = 1,
        NotAccepted = 2,
        Cancelled = 3,
        CannotBeCancelled = 4,
        Executed = 5,
    }}