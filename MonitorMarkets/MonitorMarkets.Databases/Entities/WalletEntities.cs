namespace MonitorMarkets.Databases.Entities
{
    public class WalletEntities:BaseEntity
    {
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public decimal Aviailable { get; set; }
    }
}