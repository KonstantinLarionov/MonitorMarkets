using System.Collections.Generic;
using System.Data.SqlTypes;
using MonitorMarkets.Application.Objects.Responses;
using MonitorMarkets.Application.Objects.Data.Enums;


namespace MonitorMarkets.Application.MarketsAdaptor
{
    public interface IMarketClient
    {
        #region [Public]
        ContractInfoResponse GetContractInfo(); 
        
        //OrderBookResponse GetOrderBookResponse();
        IEnumerable<KlineResponse> GetKlineResponse(string symbol, IntervalKlineType period, long startTime, long endTime);
        #endregion

        #region [Private]

        PlaceOrderResponse GetPlaceOrderResponse(string symbol, OrderTypeEnum orderType, decimal size, decimal price,
            OrderActionEnum orderAction);
        CancelOrderResponse GetCancelOrderResponse(string symbol, string orderid);
        UnfilledResponse GetActiveOrderHistory(string symbol);
        IEnumerable<TradeHistoryResponse> GetTradeHistoryResponse(string symbol, long startTime,
            long endTime, string pageSize, int limit);
        WalletInfoResponse GetWalletInfoResponse(string symbol);
        IEnumerable<MyPositionsResponse> GetMyPositionsResponse();

        #endregion

        /// <summary>
        /// Включение публичных каналов (Трейды, стакан)
        /// </summary>
        /// <returns>Результат проверки после получения первых сообщений сокета по всех каналам</returns>
        bool StartSocket();
        /// <summary>
        /// Отключение публичных каналов
        /// </summary>
        /// <returns></returns>
        bool StopSocket();
        /// <summary>
        /// Включение приватных каналов (трейды ордера позиции)
        /// </summary>
        /// <returns>Результат проверки после получения первых сообщений сокета по всех каналам</returns>
        bool StartSocketPrivate();
        /// <summary>
        /// Отключение приватных каналов
        /// </summary>
        /// <returns></returns>
        bool StopSocketPrivate();
        /// <summary>
        /// Connect
        /// </summary>
        /// <returns></returns>
        bool Connect();
        /// <summary>
        /// Установка приватных параметров, перевод биржи в приватный режим (CANBENULLSOMETHING)
        /// </summary>
        /// <param name="apikey"></param>
        /// <param name="secret"></param>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        bool ConnectPrivate(string apikey, string secret, string passphrase);
    }
}