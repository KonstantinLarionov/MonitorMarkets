namespace MonitorMarkets.Contexts.Entities
{
    public class ConnectionsKeys:BaseEntity
    {
        public string PublicKeys { get; set; }
        public string SecretKey { get; set; }
        public string PassPhrase { get; set; }
        public bool IsActive { get; }
    }
}