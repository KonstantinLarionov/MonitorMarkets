using MonitorMarkets.Application.Objects.Data.Enums;
using MonitorMarkets.DesktopDatabase.Entities;

namespace MonitorMarkets.StategyService.Requests;

public class UpdateKeyRequest
{
    public string SecretKey { get; set; }
    public string PublicKey { get; set; }
    public string PassPhrase { get; set; }
    public MarketsEnum MarketsEnum { get; }
}