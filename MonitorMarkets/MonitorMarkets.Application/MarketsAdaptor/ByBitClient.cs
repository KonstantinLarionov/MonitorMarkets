using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Globalization;
using System.Linq;
using BybitMapper.Requests;
using BybitMapper.UsdcPerpetual.RestV2;
using BybitMapper.UsdcPerpetual.RestV2.Data.Enums;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Account.Account;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Market;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Account.Order;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Account.Positions;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Account.Wallet;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Market;
using BybitMapper.UsdcPerpetual.RestV2.Responses.Market;
using MonitorMarkets.Application.Objects.Data;
using MonitorMarkets.Application.Objects.Responses;
using RestSharp;
using CancelOrderResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order.CancelOrderResponse;
using ContractInfoData = MonitorMarkets.Application.Objects.Data.ContractInfoData;
using IntervalType = BybitMapper.UsdcPerpetual.RestV2.Data.Enums.IntervalType;
using OrderBookRequest = BybitMapper.UsdcPerpetual.RestV2.Requests.Market.OrderBookRequest;
using OrderBookResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Market.OrderBookResponse;
using OrderType = BybitMapper.UsdcPerpetual.RestV2.Data.Enums.OrderType;
using PlaceOrderResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order.PlaceOrderResponse;
using QueryKlineResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Market.QueryKlineResponse;
using QueryMyPositionsResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Positions.QueryMyPositionsResponse;
using QueryOrderHistoryResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order.QueryOrderHistoryResponse;
using QueryUnfilledResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order.QueryUnfilledResponse;
using SideType = BybitMapper.UsdcPerpetual.RestV2.Data.Enums.SideType;
using TradeHistoryResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order.TradeHistoryResponse;
using WalletInfoResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Account.WalletInfoResponse;
using MonitorMarkets.Application.Extensions;
using MonitorMarkets.Application.Objects.Data.Enums;
using WebSocketSharp;
using System.Security.Authentication;
using BitgetMapper.Futures.MarketStreams.Data.Enum;
using BybitMapper.Perpetual.RestV2.Data.ObjectDTO;
using BybitMapper.UsdcPerpetual.MarketStreams;
using BybitMapper.UsdcPerpetual.MarketStreams.Data.Enum;
using BybitMapper.UsdcPerpetual.MarketStreams.Subscriptions;
using BybitMapper.UsdcPerpetual.UserStreams;
using BybitMapper.UsdcPerpetual.UserStreams.Subscriptions;
using EventPerpetualType = BybitMapper.UsdcPerpetual.MarketStreams.Data.Enum.EventPerpetualType;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    internal class ByBitClient : IMarketClient
    {
        
        private ServerTimeHelper m_ServerTimeHelper;
        private ServerTimeHelper ServerTimeHelper => m_ServerTimeHelper;

        
        private UsdcPepetualHandlerComposition m_HandlerComposition;
        private RequestArranger m_RequestArranger;
        private RestClient _restClient;

        /// <summary>
        /// Публичный конструктор класса
        /// </summary>
        public ByBitClient()
        {
            m_HandlerComposition = new UsdcPepetualHandlerComposition(new UsdcPepetualHandlerFactory());
            m_RequestArranger = new RequestArranger();
            _restClient = new RestClient();
        }

        /// <summary>
        /// Приватный конструктор класса
        /// </summary>
        /// <param name="api_key"></param>
        /// <param name="secret"></param>
        /// <param name="func"></param>
        public ByBitClient(string api_key, string secret, Func<long> func)
        {
            m_HandlerComposition = new UsdcPepetualHandlerComposition(new UsdcPepetualHandlerFactory());
            m_RequestArranger = new RequestArranger(api_key, secret, func);
            _restClient = new RestClient();
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

        #endregion

        #region [Request]

        public Objects.Responses.ContractInfoResponse GetContractInfo()
        {
            var request_prep = new ContractInfoRequest();
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            ContractInfoResponce response_obj = null;
            Objects.Responses.ContractInfoResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleContractInfoResponce(response);
                List<ContractInfoData> listData = new List<ContractInfoData>();
                
                foreach (var item in response_obj.Result)
                {
                    listData.Add(new ContractInfoData(item.Symbol, item.Status, item.BaseCoin, item.QuoteCoin,
                        item.TakerFeeRate, item.MakerFeeRate, item.MinLeverage, item.MaxLeverage, item.LeverageStep,
                        item.MinPrice, item.MaxPrice, item.TickSize, item.MinTradingQty, item.MaxTradingQty,
                        item.QtyStep, item.DeliveryFreeRate, item.DeliveryTime));
                }

                response_unt = new Objects.Responses.ContractInfoResponse(response_obj.RetCode.ToString(),
                    response_obj.RetMsg,
                    listData);

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /*public Objects.Responses.OrderBookResponse GetOrderBookResponse(string symbol)
        {
            var request_prep = new OrderBookRequest(symbol);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            OrderBookResponse response_obj = null;
            Objects.Responses.OrderBookResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleOrderBookResponse(response);

                foreach (var item in response_obj.Result)
                {
                    response_unt = new Objects.Responses.OrderBookResponse(item.Price, item.Size);
                    return response_unt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }*/

        public IEnumerable<Objects.Responses.KlineResponse> GetKlineResponse(string symbol, IntervalKlineType period, long startTime, long endTime)
        {
            var sTime = ServerTimeHelper.FromUnixMilliseconds(startTime);
            var periodreq = IntervalType.Unrecognized;
            if (TryGetIntervalKineType(period, out periodreq)) { }

            var request_prep = new QueryKlineRequest(symbol, periodreq, sTime);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryKlineResponse response_obj = null;
            IEnumerable<Objects.Responses.KlineResponse> response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryKlineResponse(response);

                response_unt = response_obj.Result.Select(x => new Objects.Responses.KlineResponse(x.OpenTime,
                    x.Open, x.Close, x.High,
                    x.Low, x.Volume));
                
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        
        
        public Objects.Responses.PlaceOrderResponse GetPlaceOrderResponse(string symbol, OrderTypeEnum orderType, decimal size, decimal price,
            OrderActionEnum orderAction, TimeInForceEnum timeInForce)
        {
            var orderSide = SideType.None;
            if (TryGetSide(orderAction, out orderSide)) { }
            
            var orderTypeReq = OrderType.None;
            if(TryGetOrderTypeReq(orderType, out orderTypeReq)) { }
            
            var request_prep = new PlaceOrderRequest(symbol, orderTypeReq, OrderFilterType.Order, orderSide,size);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            PlaceOrderResponse response_obj = null;
            Objects.Responses.PlaceOrderResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandlePlaceOrderResponse(response);

                OrderActionEnum orderAction = OrderActionEnum.Unknown;
                if (response_obj.Result.SideType == SideType.Buy)
                {
                    orderAction = OrderActionEnum.Buy;
                }

                if (response_obj.Result.SideType == SideType.Sell)
                {
                    orderAction = OrderActionEnum.Sell;
                }

                response_unt = new Objects.Responses.PlaceOrderResponse(response_obj.Result.OrderId,
                    response_obj.Result.OrderPrice, response_obj.Result.OrderQty, orderAction);

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public Objects.Responses.CancelOrderResponse GetCancelOrderResponse()
        {
            string symbol = "";
            OrderFilterType orderFilter = OrderFilterType.Order;
            var cancelOrder = new CancelOrderRequest(symbol, orderFilter: OrderFilterType.Order);
            var request = m_RequestArranger.Arrange(cancelOrder);
            string response = string.Empty;
            CancelOrderResponse response_obj = null;
            Objects.Responses.CancelOrderResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleCancelOrderResponse(response);
                response_unt = new Objects.Responses.CancelOrderResponse(response_obj.Result.OrderId);

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public Objects.Responses.UnfilledResponse GetUnfilledResponse()
        {
            CategoryType category = CategoryType.Perpetual;
            var request_prep = new QueryUnfilledRequest(category);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryUnfilledResponse response_obj = null;
            Objects.Responses.UnfilledResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryUnfilledResponse(response);
                response_unt = new Objects.Responses.UnfilledResponse();

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public Objects.Responses.OrderHistoryResponse GetOrderHistoryResponse()
        {
            CategoryType category = CategoryType.Perpetual;
            var request_prep = new QueryOrderHistoryRequest(category);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryOrderHistoryResponse response_obj = null;
            Objects.Responses.OrderHistoryResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryOrderHistoryResponse(response);

                foreach (var item in response_obj.Result.DataList)
                {
                    OrderActionEnum orderAction = OrderActionEnum.Unknown;
                    if (TryGetOrderAction(item.SideType, out orderAction)) { }
                    OrderTypeEnum orderState = OrderTypeEnum.Unknown;
                    if (TryGetOrderType(item.OrderType, out orderState)) { }
                    OrderStateEnum orderType = OrderStateEnum.None;
                    if (TryGetOrderState(item.OrderStatusType, out orderType)) { }

                    decimal remain_amount = item.Qty;

                    response_unt = new Objects.Responses.OrderHistoryResponse(item.Symbol, item.OrderId, orderAction,
                        item.Price, item.Qty, remain_amount, item.CreatedAt, orderState, TriggerTypeEnum.LastPrice,
                        orderType);
                    return response_unt;
                }

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public IEnumerable<Objects.Responses.TradeHistoryResponse> GetTradeHistoryResponse()
        {
            CategoryType category = CategoryType.Perpetual;
            int limit = 20;
            var request_prep = new TradeHistoryRequest(category, limit);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            TradeHistoryResponse response_obj = null;
            IEnumerable<Objects.Responses.TradeHistoryResponse> response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleTradeHistoryResponse(response);
                response_unt = response_obj.Result.DataList.Select(x => 
                    new Objects.Responses.TradeHistoryResponse(x.Symbol, x.TradeTime, x.TradeId, x.OrderId, x.ExecPrice,
                        x.ExecQty, x.ExecFee, x.Symbol));
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public Objects.Responses.WalletInfoResponse GetWalletInfoResponse()
        {
            var request_prep = new WalletInfoRequest();
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            WalletInfoResponse response_obj = null;
            Objects.Responses.WalletInfoResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleWalletInfoResponse(response);

                response_unt = new Objects.Responses.WalletInfoResponse("BTC", response_obj.Result.WalletBalance,
                    response_obj.Result.AvailableBalance);
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public IEnumerable<Objects.Responses.MyPositionsResponse> GetMyPositionsResponse()
        {
            CategoryType category = CategoryType.Perpetual;
            var request_prep = new QueryMyPositionsRequest(category);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryMyPositionsResponse response_obj = null;
            IEnumerable<Objects.Responses.MyPositionsResponse> response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryMyPositionsResponse(response);

                response_unt = response_obj.Result.DataList.Select(x =>
                    new Objects.Responses.MyPositionsResponse(x.Symbol, x.EntryPrice, x.Size));
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
        
        bool TryGetOrderAction(SideType in_type, out OrderActionEnum out_type)
        {
            out_type = OrderActionEnum.Unknown;
            switch (in_type)
            {
                case SideType.Buy:
                    out_type = OrderActionEnum.Buy;
                    return true;
                case SideType.Sell:
                    out_type = OrderActionEnum.Sell;
                    return true;

                default:
                    out_type = OrderActionEnum.Unknown;
                    return false;
            }
        }

        bool TryGetOrderType(OrderType in_type, out OrderTypeEnum out_type)
        {
            out_type = OrderTypeEnum.Unknown;
            switch (in_type)
            {
                case OrderType.Limit:
                    out_type = OrderTypeEnum.Limit;
                    return true;
                case OrderType.Market:
                    out_type = OrderTypeEnum.Market;
                    return true;

                default:
                    out_type = OrderTypeEnum.Unknown;
                    return false;
            }
        }

        bool TryGetOrderTypeReq(OrderTypeEnum in_type, out OrderType out_type)
        {
            out_type = OrderType.None;
            switch (in_type)
            {
                case OrderTypeEnum.Limit:
                    out_type = OrderType.Limit;
                    return true;
                case OrderTypeEnum.Market:
                    out_type = OrderType.Market;
                    return true;

                default:
                    out_type = OrderType.None;
                    return false;

            }
        }

        bool TryGetOrderState(OrderStatusType in_type, out OrderStateEnum out_type)
        {
            switch (in_type)
            {
                case OrderStatusType.None:
                    out_type = OrderStateEnum.None;
                    return true;
                case OrderStatusType.Created:
                    out_type = OrderStateEnum.Opened;
                    return true;
                case OrderStatusType.Rejected:
                    out_type = OrderStateEnum.Removed;
                    return true;
                case OrderStatusType.New:
                    out_type = OrderStateEnum.Opened;
                    return true;
                case OrderStatusType.PartiallyFilled:
                    out_type = OrderStateEnum.Partial;
                    return true;
                case OrderStatusType.Filled:
                    out_type = OrderStateEnum.Filled;
                    return true;
                case OrderStatusType.Cancelled:
                    out_type = OrderStateEnum.CancelFailed;
                    return true;

                case OrderStatusType.PendingCancel:
                    out_type = OrderStateEnum.Opened;
                    return true;
                default:
                    out_type = OrderStateEnum.None;
                    return false;
            }
        }
        

        bool TryGetIntervalKineType(IntervalKlineType in_type, out IntervalType out_type)
        {
            switch (in_type)
            {
                case IntervalKlineType.Unrecognized:
                    out_type = IntervalType.Unrecognized;
                    return true;
                case IntervalKlineType.OneMinute:
                    out_type = IntervalType.OneMinute;
                    return true;
                case IntervalKlineType.ThreeMinute:
                    out_type = IntervalType.ThreeMinute;
                    return true;
                case IntervalKlineType.FiveMinute:
                    out_type = IntervalType.FiveMinute;
                    return true;
                case IntervalKlineType.FifteenMinute:
                    out_type = IntervalType.FifteenMinute;
                    return true;
                case IntervalKlineType.ThirtyMinute:
                    out_type = IntervalType.ThirtyMinute;
                    return true;
                case IntervalKlineType.OneHour:
                    out_type = IntervalType.OneHour;
                    return true;
                case IntervalKlineType.TwoHour:
                    out_type = IntervalType.TwoHour;
                    return true;
                case IntervalKlineType.FourHour:
                    out_type = IntervalType.FourHour;
                    return true;
                case IntervalKlineType.SixHour:
                    out_type = IntervalType.SixHour;
                    return true;
                case IntervalKlineType.TwelveHours:
                    out_type = IntervalType.TwelveHours;
                    return true;
                case IntervalKlineType.OneDay:
                    out_type = IntervalType.OneDay;
                    return true;
                case IntervalKlineType.OneM:
                    out_type = IntervalType.OneM;
                    return true;
                case IntervalKlineType.OneW:
                    out_type = IntervalType.OneW;
                    return true;
                default:
                    out_type = IntervalType.Unrecognized;
                    return false;

            }
        }
        bool TryGetSide(OrderActionEnum in_type, out SideType out_type)
        {
            switch (in_type)
            {
                case OrderActionEnum.Unknown:
                    out_type = SideType.None;
                    return true;
                case OrderActionEnum.Buy:
                    out_type = SideType.Buy;
                    return true;
                case OrderActionEnum.Sell:
                    out_type = SideType.Sell;
                    return true;
                default:
                    out_type = SideType.None;
                    return false;

            }
        }

        #endregion

        #region WebSocket

        string ApiKey = string.Empty;
        string SecretKey = string.Empty;
        string Passphrase = string.Empty;
        
        WebSocket m_WebSocketPublic = null;
        WebSocket m_WebSocketPrivate = null;
        private string urlPrivateSocket = "wss://stream.bybit.com/perpetual/ws/v1/realtime_public";
        public string urlPublicSocket = "wss://stream.bybit.com/trade/option/usdc/private/v1";

        UserStreamsUsdcHandlerComposition PrivateUsdcPerpetualHandler;
        MarketStreamsUsdcPerpetualHandlerComposition PublicUsdcHandler;

        public bool StartSocket()
        {
            string symbol = "";
            var subOrderBook =
                UsdcMarketCombineStremsSubs.Create(symbol, SubType.Subscribe, PublicEndpointType.OrderBook200);
            var trade = UsdcMarketCombineStremsSubs.Create(symbol, SubType.Subscribe, PublicEndpointType.Trade);
            return true;
        }

        public bool StopSocket()
        {
            string symbol = "";
            var subOrderBook =
                UsdcMarketCombineStremsSubs.Create(symbol, SubType.Unsubscribe, PublicEndpointType.OrderBook200);
            var trade = UsdcMarketCombineStremsSubs.Create(symbol, SubType.Unsubscribe, PublicEndpointType.Trade);
            return true;
        }

        public bool StartSocketPrivate()
        {
            var auth = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Auth, ApiKey, SecretKey);
            var order = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Subscribe, UserEventType.Order);
            var position = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Subscribe, UserEventType.Position);
            var execution = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Subscribe, UserEventType.Execution);
            return true;
        }

        public bool StopSocketPrivate()
        {
            var order = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Unsubscribe, UserEventType.Order);
            var position = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Unsubscribe, UserEventType.Position);
            var execution = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Unsubscribe, UserEventType.Execution);
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

        private void OnBaseWebSocketPublicMessage(object sender, MessageEventArgs e)
        {
            var defev = PublicUsdcHandler.HandleDefaultEvent(e.Data);

            if (defev.PerpetualEventType == EventPerpetualType.OrderBook200)
            {
                var orderBook = PublicUsdcHandler.HandleOrderBookL2Event(e.Data);

                Objects.Responses.OrderBookResponse response_unt = null;

                if (orderBook.Type == DataEventType.Snapshot)
                {
                    //orderBook.DataSnap.OrderBook.Select(x => { });
                    
                    orderBook.DataSnap.OrderBook.Select(x => new Objects.Responses.OrderBookResponse(x.Price, x.Size));
                    
                }

                if (orderBook.Type == DataEventType.Delta)
                {
                    if (orderBook.DataDelta.Delete.Count > 0)
                    {
                        orderBook.DataDelta.Delete.Select(x => new Objects.Responses.OrderBookResponse(x.Price, x.Size));
                    }
                    if (orderBook.DataDelta.Insert.Count > 0)
                    {
                        orderBook.DataDelta.Insert.Select(x => new Objects.Responses.OrderBookResponse(x.Price, x.Size));
                    }

                    if (orderBook.DataDelta.Update.Count > 0)
                    {
                        orderBook.DataDelta.Update.Select(x => new Objects.Responses.OrderBookResponse(x.Price, x.Size));
                    }
                }
            }

            if (defev.PerpetualEventType == EventPerpetualType.Trade)
            {
                var trade = PublicUsdcHandler.HandleTradeEvent(e.Data);
                Objects.Responses.OnTickResponse response_unt = null;

                foreach (var item in trade.Data)
                {
                    OrderActionEnum orderAction = OrderActionEnum.Unknown;
                    if (TryGetOrderAction(item.SideType, out orderAction))
                    {
                    }

                    response_unt =
                        new Objects.Responses.OnTickResponse(item.Timestamp, item.Price, item.Size, orderAction);
                    //return response_unt;
                }
            }
        }

        private void OnBaseWebSocketPrivateMessage(object sender, MessageEventArgs e)
        {
            var defev = PublicUsdcHandler.HandleDefaultEvent(e.Data);

            if (defev.PerpetualEventType == EventPerpetualType.Order)
            {
                var order = PrivateUsdcPerpetualHandler.HandleOrderUsdcEvent(e.Data);
                Objects.Responses.PlaceOrderResponse response_unt = null;

                foreach (var item in order.Data.Result)
                {
                    OrderActionEnum orderAction = OrderActionEnum.Unknown;
                    if (TryGetOrderAction(item.SideType, out orderAction)) { }
                    
                    response_unt = new Objects.Responses.PlaceOrderResponse(item.OrderId, item.Price, item.Qty,
                        orderAction);
                }
            }

            if (defev.PerpetualEventType == EventPerpetualType.Trade)
            {
                var trade = PrivateUsdcPerpetualHandler.HandleExecutionUsdcEvent(e.Data);
                Objects.Responses.OnTickResponse response_unt = null;

                foreach (var item in trade.Data.Result)
                {
                    OrderActionEnum orderAction = OrderActionEnum.Unknown;
                    if (TryGetOrderAction(item.SideType, out orderAction)) { }

                    response_unt =
                        new Objects.Responses.OnTickResponse(item.TradeTime, item.ExecPrice, item.ExecQty, orderAction);
                    //return response_unt;
                }
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

        #endregion
    }
}