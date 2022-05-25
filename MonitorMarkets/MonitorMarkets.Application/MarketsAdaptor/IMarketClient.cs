using MonitorMarkets.Application.Objects.Responses;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    public interface IMarketClient
    {
        #region [Public]
        ContractInfoResponse GetContractInfo();
        OrderBookResponse GetOrderBookResponse();
        QueryKlineResponse GetKlineResponse();
        #endregion

        #region [Private]

        PlaceOrderResponse GetPlaceOrderResponse();
        PlaceOrderResponse GetCancelOrderResponse();
        QueryUnfilledResponse GetUnfilledResponse();
        QueryOrderHistoryResponse GetOrderHistoryResponse();
        TradeHistoryResponse GetTradeHistoryResponse();
        WalletInfoResponse GetWalletInfoResponse();
        QueryMyPositionsResponse GetMyPositionsResponse();

        #endregion
    }
}