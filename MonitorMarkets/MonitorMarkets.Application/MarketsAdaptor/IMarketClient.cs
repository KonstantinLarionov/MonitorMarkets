using MonitorMarkets.Application.Objects.Responses;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    public interface IMarketClient
    {
        ContractInfoResponse GetContractInfo();
    }
}