using System.Text.Json.Serialization;
using MonitorMarkets.DesktopDatabase.Repositories;

namespace MonitorMarkets.StategyService.Requests;

public class GetKeyRequest
{
    public string SecretKey { get; set; }
    public string PublicKey { get; set; }
    public string PassPhrase { get; set; }
}