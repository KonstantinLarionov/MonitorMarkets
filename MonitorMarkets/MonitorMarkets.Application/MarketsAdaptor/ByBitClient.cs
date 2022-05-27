using System;
using System.Collections.Generic;
using System.Globalization;
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
using BybitMapper.InversePerpetual.UserStreams.Data;
using BybitMapper.UsdcPerpetual.MarketStreams;
using BybitMapper.UsdcPerpetual.MarketStreams.Data.Enum;
using BybitMapper.UsdcPerpetual.MarketStreams.Subscriptions;
using MonitorMarkets.Application.Abstraction;
using SubType = BybitMapper.UsdcPerpetual.MarketStreams.Data.Enum.SubType;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    internal class ByBitClient : IMarketClient
    {
        private UsdcPepetualHandlerComposition m_HandlerComposition;
        private RequestArranger m_RequestArranger;
        private RestClient _restClient;

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

        public Objects.Responses.OrderBookResponse GetOrderBookResponse()
        {
            throw new NotImplementedException();
        }

        public KlineResponse GetKlineResponse()
        {
            throw new NotImplementedException();
        }

        public Objects.Responses.PlaceOrderResponse GetPlaceOrderResponse()
        {
            throw new NotImplementedException();
        }

        public Objects.Responses.CancelOrderResponse GetCancelOrderResponse()
        {
            throw new NotImplementedException();
        }

        public UnfilledResponse GetUnfilledResponse()
        {
            throw new NotImplementedException();
        }

        public OrderHistoryResponse GetOrderHistoryResponse()
        {
            throw new NotImplementedException();
        }

        public Objects.Responses.TradeHistoryResponse GetTradeHistoryResponse()
        {
            throw new NotImplementedException();
        }

        public Objects.Responses.OrderBookResponse GetOrderBookResponse(string symbol)
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
        }

        public Objects.Responses.KlineResponse GetKlineResponse(string symbol, IntervalType period, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public Objects.Responses.PlaceOrderResponse GetPlaceOrderResponse(string symbol, OrderType orderType,
            OrderFilterType orderFilter, SideType side, decimal orderQty, OrderMarkerEnum orderMarker)
        {
            throw new NotImplementedException();
        }

        public Objects.Responses.CancelOrderResponse GetCancelOrderResponse(string symbol, OrderFilterType orderFilter)
        {
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

        public Objects.Responses.UnfilledResponse GetUnfilledResponse(CategoryType category)
        {
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

        public Objects.Responses.OrderHistoryResponse GetOrderHistoryResponse(CategoryType category)
        {
            throw new NotImplementedException();
        }

        public Objects.Responses.TradeHistoryResponse GetTradeHistoryResponse(CategoryType category, int limit)
        {
            throw new NotImplementedException();

        }

        public Objects.Responses.WalletInfoResponse GetWalletInfoResponse()
        {
            throw new NotImplementedException();
        }

        public MyPositionsResponse GetMyPositionsResponse()
        {
            throw new NotImplementedException();
        }

        public bool StartSocket()
        {
            throw new NotImplementedException();
        }

        public bool StopSocket()
        {
            throw new NotImplementedException();
        }

        public bool StartSocketPrivate()
        {
            throw new NotImplementedException();
        }

        public bool StopSocketPrivate()
        {
            throw new NotImplementedException();
        }

        public bool Connect()
        {
            throw new NotImplementedException();
        }

        public bool ConnectPrivate(string apikey, string secret, string passphrase)
        {
            throw new NotImplementedException();
        }

        public Objects.Responses.MyPositionsResponse GetMyPositionsResponse(CategoryType category)
        {
            throw new NotImplementedException();

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

        #endregion

        #region WebSocket
        #endregion
        WebSocket m_WebSocketPublic = null;
        WebSocket m_WebSocketPrivate = null;
        private string urlPrivateSocket = "wss://stream.bybit.com/perpetual/ws/v1/realtime_public";
        public string urlPublicSocket = "wss://stream.bybit.com/trade/option/usdc/private/v1";

        //UserStreamsUsdcHandlerComposition PrivateUsdcPerpetualHandler;
        MarketStreamsUsdcPerpetualHandlerComposition PublicUsdcHandler;

        /*public StartSocket(string symbol)
        {
            var subOrderBook = UsdcMarketCombineStremsSubs.Create(symbol, SubType.Subscribe, PublicEndpointType.OrderBook200);
            var trade = UsdcMarketCombineStremsSubs.Create(symbol, SubType.Subscribe, PublicEndpointType.Trade);
        }

        public StopSocket(string symbol)
        {
            var subOrderBook = UsdcMarketCombineStremsSubs.Create(symbol, SubType.Unsubscribe, PublicEndpointType.OrderBook200);
            var trade = UsdcMarketCombineStremsSubs.Create(symbol, SubType.Unsubscribe, PublicEndpointType.Trade);

        }

        private StartSocketPrivate(string _apiKey, string _secretkey)
        {
            var auth = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Auth, _apiKey, _secretkey);
            var order = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Subscribe, UserEventType.Order);
            var position = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Subscribe, UserEventType.Position);
            var execution  = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Subscribe, UserEventType.Execution);
        }

        private StopSocketPrivate()
        {
            var order = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Unsubscribe, UserEventType.Order);
            var position = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Unsubscribe, UserEventType.Position);
            var execution  = CombineStremsSubsUsdcPerpetualUser.Create(SubType.Unsubscribe, UserEventType.Execution);
        }

        public Connect()
        {
            m_WebSocketPublic = new WebSocket(urlPublicSocket) { EmitOnPing = true };
            m_WebSocketPublic.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12; 
            m_WebSocketPublic.Log.File = null;
            m_WebSocketPublic.OnOpen += WebSocket_OnOpen;
            m_WebSocketPublic.OnClose += WebSocket_OnClose;
            m_WebSocketPublic.OnError += WebSocket_OnError;
            m_WebSocketPublic.OnMessage += WebSocket_OnMessage;
        }

        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            var defev = PublicUsdcHandler.HandleDefaultEvent(e.Data);

            if (defev.PerpetualEventType == EventPerpetualType.OrderBook200)
            {
                var orderBook = PublicUsdcHandler.HandleOrderBookL2Event(e.Data);

                Objects.Responses.OrderBookResponse response_unt = null;
                
                if (orderBook.Type == DataEventType.Snapshot)
                {
                    foreach (var item in orderBook.DataSnap.OrderBook)
                    {
                        response_unt = new Objects.Responses.OrderBookResponse(item.Price, item.Size);
                        return response_unt;

                    }

                }
                if (orderBook.Type == DataEventType.Delta)
                {
                    if (orderBook.DataDelta.Delete.Count > 0)
                    {
                        foreach (var item in orderBook.DataDelta.Delete)
                        {
                            response_unt = new Objects.Responses.OrderBookResponse(item.Price, item.Size);
                            return response_unt;

                        }
                    }
                    if (orderBook.DataDelta.Insert.Count > 0)
                    {
                        foreach (var item in orderBook.DataDelta.Insert)
                        {
                            response_unt = new Objects.Responses.OrderBookResponse(item.Price, item.Size);
                            return response_unt;

                        }
                    }
                    if (orderBook.DataDelta.Update.Count > 0)
                    {
                        foreach (var item in orderBook.DataDelta.Update)
                        {
                            response_unt = new Objects.Responses.OrderBookResponse(item.Price, item.Size);
                            return response_unt;

                        }
                    }

                }
            }
        }

        private ConnectPrivate()
        {
            m_WebSocketPrivate = new WebSocket(urlPrivateSocket) { EmitOnPing = true };
            m_WebSocketPrivate.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12; 
            m_WebSocketPrivate.Log.File = null;
            m_WebSocketPrivate.OnOpen += WebSocket_OnOpen;
            m_WebSocketPrivate.OnClose += WebSocket_OnClose;
            m_WebSocketPrivate.OnError += WebSocket_OnError;
            m_WebSocketPrivate.OnMessage += WebSocket_OnMessage;
        }
        
        
        #endregion
    }*/
    }
}