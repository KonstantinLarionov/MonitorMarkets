namespace MonitorMarkets.Context.Entities;

public class ConnectionKeys:BaseEntity
{
    public string PublicKeys { get; set; }
    public string SecretKey { get; set; }
    public string PassPhrase { get; set; }
    public bool IsActive { get; }
}