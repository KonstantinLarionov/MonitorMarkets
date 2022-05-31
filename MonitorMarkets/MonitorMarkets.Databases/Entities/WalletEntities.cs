namespace MonitorMarkets.Databases.Entities
{
    public class WalletEntities
    {
        public string Id { get; set; }
        public string Currency { get; }
        public decimal Balance { get; }
        public decimal Aviailable { get; }
    }
}