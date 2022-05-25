using MonitorMarkets.Application.Objects.Responses;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    public interface IMarketClient
    {
        #region [Public]
        ContractInfoResponse GetContractInfo();
        OrderBookResponse GetOrderBookResponse();
        KlineResponse GetKlineResponse();
        #endregion

        #region [Private]

        PlaceOrderResponse GetPlaceOrderResponse();
        PlaceOrderResponse GetCancelOrderResponse();
        UnfilledResponse GetUnfilledResponse();
        OrderHistoryResponse GetOrderHistoryResponse();
        TradeHistoryResponse GetTradeHistoryResponse();
        WalletInfoResponse GetWalletInfoResponse();
        MyPositionsResponse GetMyPositionsResponse();

        #endregion
    }
}