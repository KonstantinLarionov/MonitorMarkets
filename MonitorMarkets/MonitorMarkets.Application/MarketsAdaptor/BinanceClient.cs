using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using BinanceMapper.Futures.V1;
using BinanceMapper.Requests;
using MonitorMarkets.Application.Abstraction;
using MonitorMarkets.Application.Objects.Data.Enums;
using MonitorMarkets.Application.Objects.Responses;
using RestSharp;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    public class BinanceClient : IMarketClient
    {
        private readonly FuturesApiV1HandlerComposition _composition =
            new FuturesApiV1HandlerComposition(new FuturesApiV1HandlerFactory());
        private RequestArranger _requestArranger;
        private RestClient _restClient;
        
        public ContractInfoResponse GetContractInfo()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<KlineResponse> GetKlineResponse(string symbol, IntervalKlineType period, long startTime, long endTime)
        {
            throw new NotImplementedException();
        }

        public PlaceOrderResponse GetPlaceOrderResponse(string symbol, OrderTypeEnum orderType, decimal size, decimal price,
            OrderActionEnum orderAction, SideTypeOrderEnum sideOrderType)
        {
            throw new NotImplementedException();
        }

        public CancelOrderResponse GetCancelOrderResponse(string symbol, string orderid)
        {
            throw new NotImplementedException();
        }

        public UnfilledResponse GetActiveOrderHistory(string symbol)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeHistoryResponse> GetTradeHistoryResponse(string symbol, long startTime, long endTime, string pageSize, int limit)
        {
            throw new NotImplementedException();
        }

        public WalletInfoResponse GetWalletInfoResponse(string symbol)
        {
            throw new NotImplementedException();
        }

        IEnumerable<MyPositionsResponse> IMarketClient.GetMyPositionsResponse()
        {
            throw new NotImplementedException();
        }

        public OrderBookResponse GetOrderBookResponse()
        {
            throw new System.NotImplementedException();
        }

        public KlineResponse GetKlineResponse()
        {
            throw new System.NotImplementedException();
        }

        public PlaceOrderResponse GetPlaceOrderResponse()
        {
            throw new System.NotImplementedException();
        }

        public CancelOrderResponse GetCancelOrderResponse()
        {
            throw new System.NotImplementedException();
        }

        public UnfilledResponse GetUnfilledResponse()
        {
            throw new System.NotImplementedException();
        }

        public OrderHistoryResponse GetOrderHistoryResponse()
        {
            throw new System.NotImplementedException();
        }

        public TradeHistoryResponse GetTradeHistoryResponse()
        {
            throw new System.NotImplementedException();
        }

        public WalletInfoResponse GetWalletInfoResponse()
        {
            throw new System.NotImplementedException();
        }

        public MyPositionsResponse GetMyPositionsResponse()
        {
            throw new System.NotImplementedException();
        }

        public bool StartSocket()
        {
            throw new System.NotImplementedException();
        }

        public bool StopSocket()
        {
            throw new System.NotImplementedException();
        }

        public bool StartSocketPrivate(Func<long> timestamp)
        {
            throw new NotImplementedException();
        }

        public bool StartSocketPrivate()
        {
            throw new System.NotImplementedException();
        }

        public bool StopSocketPrivate()
        {
            throw new System.NotImplementedException();
        }

        public bool Connect()
        {
            if (MarketsInfo.Binance_BASEURL is null)
                return false;
            
            if (_composition is null)
                return false;
            
            _restClient = new RestClient(MarketsInfo.Binance_BASEURL);
            _requestArranger = new RequestArranger();
            
            return true;
        }

        public bool ConnectPrivate(string apikey, string secret, string passphrase)
        {
            if (apikey is null)
                return false;
            
            if (secret is null)
                return false;
            
            if (passphrase is null)
                return false;
            
            _restClient = new RestClient(MarketsInfo.Binance_BASEURL);
            _requestArranger = new RequestArranger(apikey, secret, GetNonce);

            return true;
        }

        public event EventHandler<IEnumerable<OrderBookResponse>> OrderbookEvent;
        public event EventHandler<OnTickResponse> OntickPublicEvent;
        public event EventHandler<OnTickResponse> OntickPrivateEvent;
        public event EventHandler<PlaceOrderResponse> PlaceorderEvent;
        public event EventHandler<MyPositionsResponse> MypositionsEvent;
        public event EventHandler<PlaceOrderResponse> PlaceorderPrivateEvent;

        /// <summary>
        /// Сюда надо синхронизацию времени
        /// </summary>
        /// <returns></returns>
        long GetNonce() => 0;
    }
}