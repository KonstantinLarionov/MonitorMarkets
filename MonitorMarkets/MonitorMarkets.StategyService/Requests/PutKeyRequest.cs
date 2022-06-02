namespace MonitorMarkets.StategyService.Requests;

public class PutKeyRequest
{
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
    public string PassPhrase { get; set; }
}