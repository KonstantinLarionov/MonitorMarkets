using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using BitgetMapper.Futures.RestAPI;
using BitgetMapper.Futures.RestAPI.Data.DTO;
using BitgetMapper.Futures.RestAPI.Data.DTO.Enum;
using BitgetMapper.Futures.RestAPI.Data.DTO.Market;
using BitgetMapper.Futures.RestAPI.Requests.Account;
using BitgetMapper.Futures.RestAPI.Requests.Market;
using BitgetMapper.Futures.RestAPI.Responses.Account;
using BitGetMapper.Futures.RestAPI.Responses.Account;
using BitgetMapper.Futures.RestAPI.Responses.Market;
using BitgetMapper.Requests;
using BitgetMapper.Futures.MarketStreams;
using BitgetMapper.Futures.AccountStreams;
using BitgetMapper.Futures.AccountStreams.Data.Enum.PositionsDataEnums;
using BitgetMapper.Futures.MarketStreams.Data.Enum;
using BitgetMapper.Futures.MarketStreams.Data.Enum.ArgDataEnum;
using BitgetMapper.Futures.MarketStreams.Subscriptions;
using MonitorMarkets.Application.Abstraction;
using MonitorMarkets.Application.Extensions;
using MonitorMarkets.Application.Objects.Data;
using MonitorMarkets.Application.Objects.Data.Enums;
using MonitorMarkets.Application.Objects.Responses;
using RestSharp;
using CancelOrderResponse = BitgetMapper.Futures.RestAPI.Responses.Account.CancelOrderResponse;
using GetAllSymbolsResponse = BitgetMapper.Futures.RestAPI.Responses.Market.GetAllSymbolsResponse;
using GetDepthResponse = BitgetMapper.Futures.RestAPI.Responses.Market.GetDepthResponse;
using GetHistoryOrderResponse = BitGetMapper.Futures.RestAPI.Responses.Account.GetHistoryOrderResponse;
using GetOpenOrderResponse = BitGetMapper.Futures.RestAPI.Responses.Account.GetOpenOrderResponse;
using GetSingleAccountResponse = BitgetMapper.Futures.RestAPI.Responses.Account.GetSingleAccountResponse;
using OrderTypeEnum = BitGetMapper.Futures.RestAPI.Data.DTO.Enum.OrderTypeEnum;
using PlaceOrderResponse = BitgetMapper.Futures.RestAPI.Responses.Account.PlaceOrderResponse;
using WebSocketSharp;
using SideType = BitGetMapper.Futures.RestAPI.Data.DTO.Enum.SideType;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    public class BitgetClient : IMarketClient
    {
        readonly RequestArranger _requestArranger;
        private FuturesHanlderComposition _composition;
        private RestClient _restClient;
        readonly ServerTimeHelper m_ServerTimeHelper;
        public ServerTimeHelper ServerTimeHelper => m_ServerTimeHelper;

        public BitgetClient(string rest_url)
        {
            _restClient = new RestClient(rest_url);
            _requestArranger = new RequestArranger();
            _composition = new FuturesHanlderComposition(new FuturesHandlerFactory());
        }

        public BitgetClient(string api_key, string secret_key, string passphrase, Func<long> funcTime, string rest_url)
        {
            _restClient = new RestClient(rest_url);
            _requestArranger = new RequestArranger(api_key, secret_key, passphrase, funcTime);
            _composition = new FuturesHanlderComposition(new FuturesHandlerFactory());
        }

        #region [Base]

        string SendRestRequest(IRequestContent message)
        {
            Method method;

            switch (message.Method)
            {
                case RequestMethod.GET:
                    method = Method.GET;
                    break;
                case RequestMethod.POST:
                    method = Method.POST;
                    break;
                case RequestMethod.PUT:
                    method = Method.PUT;
                    break;
                case RequestMethod.DELETE:
                    method = Method.DELETE;
                    break;
                default:
                    throw new NotImplementedException("Unknown request method");
            }

            var request = new RestRequest(message.Query, method);
            if (message.Body != null)
            {
                request.RequestFormat = DataFormat.Json;
                request.AddBody(message.Body);
            }

            if (message.Headers != null)
            {
                foreach (var header in message.Headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            return _restClient.Execute(request).Content;
        }

        internal delegate void Log_Dlg(string sender, string message);

        internal Log_Dlg Log;
        internal bool LogResponseEnabled = false;
        internal bool LogExEnabled = false;

        /// <summary>
        /// 
        /// </summary>
        void OnLogResponse(string response)
        {
            if (LogResponseEnabled)
            {
                Log?.Invoke("RestClient", string.Concat("Response: ", response));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void OnLogEx(Exception ex, string response = null)
        {
            if (LogExEnabled)
            {
                Log?.Invoke("RestClient",
                    string.Concat("Exception: ", ex.Message, "; ", ex?.InnerException, " - ", response));
            }
        }

        #endregion

        #region [Requests]

        #region [Market]

        public ContractInfoResponse GetContractInfo()
        {
            var allSymbols = new GetAllSymbolsRequest(ProductTypeEnum.Umcbl);
            var request = _requestArranger.Arrange(allSymbols);
            GetAllSymbolsResponse response_obj = null;
            string response = string.Empty;
            ContractInfoResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetAllSymbolsResponse(response);
                List<ContractInfoData> listData = new List<ContractInfoData>();
                foreach (var item in response_obj.Data)
                {
                    listData.Add(new ContractInfoData(item.Symbol, null, item.BaseCoin, item.QuoteCoin,
                        item.TakerFeeRate, item.MakerFeeRate, null, null, null, null, null, null, null, null, null,
                        null, null));
                }

                response_unt = new ContractInfoResponse(response_obj.Code, response_obj.Msg, listData);
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public IEnumerable<Objects.Responses.KlineResponse> GetKlineResponse(string symbol, IntervalKlineType period,
            long startTime, long endTime)
        {
            var periodreq = GranularityEnum.None;
            if (TryGetIntervalKineType(period, out periodreq))
            {
            }

            var sTime = ServerTimeHelper.FromUnixMilliseconds(startTime);
            var eTime = ServerTimeHelper.FromUnixMilliseconds(endTime);

            var candleData = new GetCandleDataRequest(symbol, periodreq, sTime, eTime);
            var request = _requestArranger.Arrange(candleData);
            List<List<Decimal>> response_obj = null;
            string response = string.Empty;
            IEnumerable<Objects.Responses.KlineResponse> response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetCandleDataResponse(response);

                response_unt = response_obj.Select(x =>
                    new Objects.Responses.KlineResponse(Convert.ToInt64(x[0]), x[1], x[2], x[3], x[4], x[5]));
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public ServerTimeResponse ServerTimeRequest()
        {
            var serverTime = new ServerTimeRequest();
            var request = _requestArranger.Arrange(serverTime);
            ServerTimeResponse response_obj = null;

            string response = string.Empty;
            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleServerTimeResponse(response);
                return response_obj;
            }
            catch (Exception ex)
            {
                OnLogEx(ex, response);
            }

            return response_obj;
        }

        #endregion

        #region [Account]

        public Objects.Responses.CancelOrderResponse GetCancelOrderResponse(string symbol, string orderid)
        {
            var cancelOrder = new CancelOrderRequest(symbol, "USDT", orderid);
            var request = _requestArranger.Arrange(cancelOrder);
            string response = string.Empty;
            CancelOrderResponse response_obj = null;
            Objects.Responses.CancelOrderResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleCancelOrderResponse(response);
                response_unt = new Objects.Responses.CancelOrderResponse(response_obj.Data.OrderId);

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public Objects.Responses.WalletInfoResponse GetWalletInfoResponse(string symbol)
        {
            var singleAccount = new GetSingleAccountRequest(symbol, "USDT");
            var request = _requestArranger.Arrange(singleAccount);
            GetSingleAccountResponse response_obj = null;
            Objects.Responses.WalletInfoResponse response_unt = null;

            string response = string.Empty;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetSingleAccountResponse(response);
                response_unt = new Objects.Responses.WalletInfoResponse("USDT", response_obj.Data.UsdtEquity,
                    response_obj.Data.Available);
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public Objects.Responses.PlaceOrderResponse GetPlaceOrderResponse(string symbol,
            Objects.Data.Enums.OrderTypeEnum orderType, decimal size, decimal price,
            OrderActionEnum orderAction, SideTypeOrderEnum sideOrderTypeEnum)
        {
            var side = BitGetMapper.Futures.RestAPI.Data.DTO.Enum.OrderTypeEnum.None;
            if (TryGetFromOrderType(orderType, out side))
            {
            }

            var sideOrder = SideType.None;
            if (TryGetSideType(sideOrderTypeEnum, out sideOrder))
            {
            }

            var placeOrder = new PlaceOrderRequest(symbol, "USDT", size, sideOrder, side);
            var request = _requestArranger.Arrange(placeOrder);
            PlaceOrderResponse response_obj = null;
            string response = string.Empty;
            Objects.Responses.PlaceOrderResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandlePlaceOrderResponse(response);

                response_unt =
                    new Objects.Responses.PlaceOrderResponse(response_obj.Data.OrderId, 0, 0, OrderActionEnum.Unknown);

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /*public Objects.Responses.OrderBookResponse GetOrderBookResponse()
        {
        }*/
        public Objects.Responses.UnfilledResponse GetActiveOrderHistory(string symbol)
        {
            var placeOrder = new GetOpenOrderRequest(symbol);
            var request = _requestArranger.Arrange(placeOrder);
            GetOpenOrderResponse response_obj = null;
            string response = string.Empty;
            Objects.Responses.UnfilledResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetOpenOrderResponse(response);

                response_unt = new Objects.Responses.UnfilledResponse();

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public IEnumerable<TradeHistoryResponse> GetTradeHistoryResponse(string symbol, long startTime,
            long endTime, string pageSize, int limit)
        {
            var sTime = ServerTimeHelper.FromUnixMilliseconds(startTime);
            var eTime = ServerTimeHelper.FromUnixMilliseconds(endTime);
            var placeOrder = new GetHistoryOrderRequest(symbol, sTime, eTime, pageSize);
            var request = _requestArranger.Arrange(placeOrder);
            GetHistoryOrderResponse response_obj = null;
            string response = string.Empty;
            IEnumerable<TradeHistoryResponse> response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetHistoryOrderResponse(response);

                response_unt = response_obj.Data.OrderList.Select(x =>
                    new Objects.Responses.TradeHistoryResponse(x.Symbol, x.Ctime, x.OrderId, x.Price, x.Size, x.Fee,
                        x.MarginCoin));
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /*public Objects.Responses.TradeHistoryResponse GetTradeHistoryResponse()
        {
        }*/
        public IEnumerable<Objects.Responses.MyPositionsResponse> GetMyPositionsResponse()
        {
            var placeOrder = new GetAllPositionRequest(ProductTypeEnum.Umcbl);
            var request = _requestArranger.Arrange(placeOrder);
            GetAllPositionResponse response_obj = null;
            string response = string.Empty;
            IEnumerable<Objects.Responses.MyPositionsResponse> response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetAllPositionResponse(response);

                response_unt = response_obj.Data.Select(x =>
                    new Objects.Responses.MyPositionsResponse(x.Symbol, x.AverageOpenPrice, x.Margin));
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        #endregion

        #endregion

        #region WebSocket

        public MarketStreamsFuturesHandlerComposition MarketHandler =
            new MarketStreamsFuturesHandlerComposition(new MarketStreamsFuturesHandlerFactory());

        public AccountStreamsFuturesHandlerComposition AccountHandler =
            new AccountStreamsFuturesHandlerComposition(new AccountStreamsFuturesHandlerFactory());

        private string urlPrivateSocket = "wss://ws.bitget.com/mix/v1/stream";

        string ApiKey = string.Empty;
        string SecretKey = string.Empty;
        string Passphrase = string.Empty;
        private string urlPublicSocket = "wss://ws.bitget.com/mix/v1/stream";

        WebSocket m_WebSocketPublic = null;
        WebSocket m_WebSocketPrivate = null;

        public bool StartSocket()
        {
            string instId = "";
            var subOrderBook =
                CombineStreamsSubs.CreatePublicEvent(EventTypeEnum.Subscirbe, InstTypeEnum.Mc, ChannelEnum.Books,
                    instId);
            var trade = CombineStreamsSubs.CreatePublicEvent(EventTypeEnum.Subscirbe, InstTypeEnum.Mc,
                ChannelEnum.Trade, instId);
            return true;
        }

        public bool StopSocket()
        {
            return true;
        }

        public bool StartSocketPrivate(Func<long> timestamp)
        {
            string instId = "";
            var auth =
                CombineStreamsSubs.Create(EventTypeEnum.Subscirbe, ApiKey, SecretKey, Passphrase, timestamp);
            var account = 
                CombineStreamsSubs.CreatePrivateEvent(EventTypeEnum.Subscirbe, InstTypeEnum.Umcbl, ChannelEnum.Account);
            var order = CombineStreamsSubs.CreatePrivateEvent(EventTypeEnum.Subscirbe, InstTypeEnum.Umcbl,
                ChannelEnum.Orders);
            var position =
                CombineStreamsSubs.CreatePrivateEvent(EventTypeEnum.Subscirbe, InstTypeEnum.Umcbl,
                    ChannelEnum.Positions);

            return true;
        }

        public bool StopSocketPrivate()
        {
            var account =
                CombineStreamsSubs.CreatePrivateEvent(EventTypeEnum.Subscirbe, InstTypeEnum.Umcbl, ChannelEnum.Account);
            var order = CombineStreamsSubs.CreatePrivateEvent(EventTypeEnum.Subscirbe, InstTypeEnum.Umcbl,
                ChannelEnum.Orders);
            var position =
                CombineStreamsSubs.CreatePrivateEvent(EventTypeEnum.Subscirbe, InstTypeEnum.Umcbl,
                    ChannelEnum.Positions);

            return true;
        }

        public bool Connect()
        {
            m_WebSocketPublic = new WebSocket(urlPublicSocket) { EmitOnPing = true };
            m_WebSocketPublic.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            m_WebSocketPublic.Log.File = null;
            //m_WebSocketPublic.OnOpen += WebSocket_OnOpen;
            //m_WebSocketPublic.OnClose += WebSocket_OnClose;
            //m_WebSocketPublic.OnError += WebSocket_OnError;
            m_WebSocketPublic.OnMessage += WebSocket_OnMessage;
            return true;
        }

        void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            if (sender == m_WebSocketPrivate)
            {
                OnBaseWebSocketPrivateMessage(sender, e);
            }

            if (sender == m_WebSocketPublic)
            {
                OnBaseWebSocketPublicMessage(sender, e);
            }
        }


        public bool ConnectPrivate(string apiKey, string secretKey, string passphrase)
        {
            ApiKey = apiKey;
            SecretKey = secretKey;
            Passphrase = passphrase;
            m_WebSocketPrivate = new WebSocket(urlPrivateSocket) { EmitOnPing = true };
            m_WebSocketPrivate.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            m_WebSocketPrivate.Log.File = null;
            //m_WebSocketPrivate.OnOpen += WebSocket_OnOpen;
            //m_WebSocketPrivate.OnClose += WebSocket_OnClose;
            //m_WebSocketPrivate.OnError += WebSocket_OnError;
            m_WebSocketPrivate.OnMessage += WebSocket_OnMessage;
            return true;
        }

        public event EventHandler<IEnumerable<MonitorMarkets.Application.Objects.Responses.OrderBookResponse>> OrderbookEvent;
        public event EventHandler<OnTickResponse> OntickPublicEvent;
        public event EventHandler<OnTickResponse> OntickPrivateEvent;
        public event EventHandler<MonitorMarkets.Application.Objects.Responses.PlaceOrderResponse> PlaceorderEvent;
        public event EventHandler<MyPositionsResponse> MypositionsEvent;
        public event EventHandler<MonitorMarkets.Application.Objects.Responses.PlaceOrderResponse>
            PlaceorderPrivateEvent;
        
        private void OnBaseWebSocketPublicMessage(object sender, MessageEventArgs e)
        {
            var defev = MarketHandler.HandleDefaultEvent(e.Data);

            if (defev.PerpetualEventType == EventPerpetualType.OrderBook)
            {
                var orderBook = MarketHandler.HandleOrderBookEvent(e.Data);

                IEnumerable<Objects.Responses.OrderBookResponse> response_unt = null;

                if (orderBook.Data == MarketHandler)
                {
                    //orderBook.DataSnap.OrderBook.Select(x => { });

                    foreach (var item in orderBook.Data)
                    {
                        var orderBookAsksResponses = item.Asks.Select(x =>
                            new Objects.Responses.OrderBookResponse(x[0], -x[1]));
                        var orderBookBidsResponses = item.Bids.Select(x =>
                            new Objects.Responses.OrderBookResponse(x[0], -x[1]));
                        
                        OrderbookEvent?.Invoke(sender, orderBookAsksResponses);
                        OrderbookEvent?.Invoke(sender, orderBookBidsResponses);

                        //return response_unt;
                    }
                    
                }
            }

            if (defev.PerpetualEventType == EventPerpetualType.Trades)
            {
                var trade = MarketHandler.HandleTradesChannelEvent(e.Data);
                Objects.Responses.OnTickResponse response_unt = null;

                foreach (var item in trade.Data)
                {
                    OrderActionEnum orderAction = OrderActionEnum.Unknown;
                    if (TryGetOrderAction(item.SideEnum, out orderAction))
                    {
                    }

                    var onTickResponse =
                        new Objects.Responses.OnTickResponse(item.Ts, item.Px, item.Sz, orderAction);
                    OntickPublicEvent?.Invoke(sender, onTickResponse);

                    //return response_unt;
                }
            }
        }

        private void OnBaseWebSocketPrivateMessage(object sender, MessageEventArgs e)
        {
            var defev = MarketHandler.HandleDefaultEvent(e.Data);

            if (defev.PerpetualEventType == EventPerpetualType.Order)
            {
                var order = AccountHandler.HandleOrderEvent(e.Data);
                Objects.Responses.PlaceOrderResponse response_unt = null;

                foreach (var item in order.Data)
                {
                    OrderActionEnum orderAction = OrderActionEnum.Unknown;
                    if (TryGetOrderAction(item.SideEnum, out orderAction))
                    {
                    }

                    var placeOrderPrivateResponse = new Objects.Responses.PlaceOrderResponse(item.OrdId, item.Px, item.Sz, orderAction);
                    PlaceorderPrivateEvent?.Invoke(sender, placeOrderPrivateResponse);
                }
            }

            /*if (defev.PerpetualEventType == EventPerpetualType.Account)
            {
                var trade = AccountHandler.HandleAccountEvent(e.Data);
                Objects.Responses.OnTickResponse response_unt = null;

                foreach (var item in trade.Data)
                {
                    OrderActionEnum orderAction = OrderActionEnum.Unknown;
                    if (TryGetOrderAction(item.SideEnum, out orderAction))
                    {
                    }

                    response_unt =
                        new Objects.Responses.OnTickResponse(item.TradeTime, item.ExecPrice, item.ExecQty, orderAction);
                    //return response_unt;
                }
            }*/

            if (defev.PerpetualEventType == EventPerpetualType.Positions)
            {
                var positions = AccountHandler.HandlePositionsEvent(e.Data);
                Objects.Responses.MyPositionsResponse response_unt = null;

                foreach (var item in positions.Data)
                {
                    var myPositionsResponse =
                        new Objects.Responses.MyPositionsResponse(item.MarginCoin, item.AverageOpenPrice, item.Margin);
                    MypositionsEvent?.Invoke(sender, myPositionsResponse);
                    //return response_unt;
                }
            }
        }


        bool TryGetOrderAction(SideEnum in_type, out OrderActionEnum out_type)
        {
            out_type = OrderActionEnum.Unknown;
            switch (in_type)
            {
                case SideEnum.Buy:
                    out_type = OrderActionEnum.Buy;
                    return true;
                case SideEnum.Sell:
                    out_type = OrderActionEnum.Sell;
                    return true;

                default:
                    out_type = OrderActionEnum.Unknown;
                    return false;
            }
        }

        bool TryGetFromOrderType(Objects.Data.Enums.OrderTypeEnum in_type,
            out BitGetMapper.Futures.RestAPI.Data.DTO.Enum.OrderTypeEnum out_type)
        {
            out_type = BitGetMapper.Futures.RestAPI.Data.DTO.Enum.OrderTypeEnum.None;
            switch (in_type)
            {
                case Objects.Data.Enums.OrderTypeEnum.Limit:
                    out_type = BitGetMapper.Futures.RestAPI.Data.DTO.Enum.OrderTypeEnum.Limit;
                    return true;
                case Objects.Data.Enums.OrderTypeEnum.Market:
                    out_type = BitGetMapper.Futures.RestAPI.Data.DTO.Enum.OrderTypeEnum.Market;
                    return true;
                default:
                    out_type = OrderTypeEnum.None;
                    return false;
            }
        }

        bool TryGetIntervalKineType(IntervalKlineType in_type, out GranularityEnum out_type)
        {
            switch (in_type)
            {
                case IntervalKlineType.Unrecognized:
                    out_type = GranularityEnum.None;
                    return true;
                case IntervalKlineType.OneMinute:
                    out_type = GranularityEnum.Minute;
                    return true;
                case IntervalKlineType.FiveMinute:
                    out_type = GranularityEnum.FiveMinute;
                    return true;
                case IntervalKlineType.FifteenMinute:
                    out_type = GranularityEnum.FiveteenMinute;
                    return true;
                case IntervalKlineType.ThirtyMinute:
                    out_type = GranularityEnum.ThrityMinute;
                    return true;
                case IntervalKlineType.OneHour:
                    out_type = GranularityEnum.Hour;
                    return true;
                case IntervalKlineType.FourHour:
                    out_type = GranularityEnum.FourHour;
                    return true;
                case IntervalKlineType.OneDay:
                    out_type = GranularityEnum.Day;
                    return true;
                case IntervalKlineType.OneW:
                    out_type = GranularityEnum.Week;
                    return true;
                default:
                    out_type = GranularityEnum.None;
                    return false;
            }
        }

        bool TryGetSideType(SideTypeOrderEnum in_type, out SideType out_type)
        {
            out_type = SideType.None;
            switch (in_type)
            {
                case SideTypeOrderEnum.CloseLong:
                    out_type = SideType.CloseLong;
                    return true;
                case SideTypeOrderEnum.CloseShort:
                    out_type = SideType.CloseShort;
                    return true;
                case SideTypeOrderEnum.OpenLong:
                    out_type = SideType.OpenLong;
                    return true;
                case SideTypeOrderEnum.OpenShort:
                    out_type = SideType.OpenShort;
                    return true;

                default:
                    out_type = SideType.None;
                    return false;
            }
        }
    }

    #endregion
}
