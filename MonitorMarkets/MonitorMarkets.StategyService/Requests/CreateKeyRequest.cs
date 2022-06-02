namespace MonitorMarkets.StategyService.Requests;

public class CreateKeyRequest
{
    public string SecretKey { get; set; }
    public string PublicKey { get; set; }
    public string PassPhrase { get; set; }
}