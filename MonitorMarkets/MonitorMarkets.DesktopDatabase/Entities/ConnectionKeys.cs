using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.DesktopDatabase.Entities;

public class ConnectionKeys:BaseEntity
{
    public string PublicKeys { get; set; }
    public string SecretKeys { get; set; }
    public string PassPhrase { get; set; }
    public bool IsActive { get; }
    public MarketsEnum MarketsEnum { get; set; }
}