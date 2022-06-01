namespace MonitorMarkets.Databases.Entities
{
    public class WalletEntities
    {
        public string Id { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public decimal Aviailable { get; set; }
    }
}