namespace MonitorMarkets.DesktopDatabase.Entities;

public class ConnectionKeys:BaseEntity
{
    public string PublicKeys { get; set; }
    public string SecretKeys { get; set; }
    public string PassPhrase { get; set; }
    public bool IsActive { get; }
}