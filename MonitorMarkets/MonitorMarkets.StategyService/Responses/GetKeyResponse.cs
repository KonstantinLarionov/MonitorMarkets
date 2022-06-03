namespace MonitorMarkets.StategyService.Responses;

public class GetKeyResponse:BaseResponse
{
    public string SecretKey { get; set; }
    public string PublicKey { get; set; }
    public string PassPhrase { get; set; }
    public Enum MarketsEnum { get;}
}