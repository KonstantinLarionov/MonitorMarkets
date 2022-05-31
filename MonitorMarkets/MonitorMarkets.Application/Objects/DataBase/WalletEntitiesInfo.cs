namespace MonitorMarkets.Application.Objects.DataBase
{
    public class WalletEntitiesInfo
    {
        public string Id { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public decimal Aviailable { get; set; }
    }
}