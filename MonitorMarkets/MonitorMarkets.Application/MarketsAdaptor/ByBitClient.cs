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

namespace MonitorMarkets.Application.MarketsAdaptor
{
    internal class ByBitClient : IMarketClient
    {
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
                { request.AddHeader(header.Key, header.Value); }
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
            var request_prep = new QueryKlineRequest(symbol, period, startTime);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryKlineResponse response_obj = null;
            Objects.Responses.KlineResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryKlineResponse(response);

                foreach (var item in response_obj.Result)
                {
                    response_unt = new Objects.Responses.KlineResponse(FromUnixMilliseconds(item.OpenTime), item.Close, item.High, item.Low, item.Volume);
                    return response_unt;

                }
            }
            catch (Exception ex)    
            {
                return null;
            }
            return null;
        }
        public Objects.Responses.PlaceOrderResponse GetPlaceOrderResponse(string symbol, OrderType orderType, OrderFilterType orderFilter, SideType side, decimal orderQty, OrderMarkerEnum orderMarker)
        {
            var request_prep = new PlaceOrderRequest(symbol, orderType, orderFilter, side, orderQty);
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

                response_unt = new Objects.Responses.PlaceOrderResponse(response_obj.Result.OrderId, response_obj.Result.OrderPrice, response_obj.Result.OrderQty, orderAction, orderMarker);

                return response_unt;

            }
            catch (Exception ex)    
            {
                return null;
            }

            return null;

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
                    
                    response_unt = new Objects.Responses.OrderHistoryResponse(item.Symbol, item.OrderId, orderAction, item.Price, item.Qty, remain_amount, item.CreatedAt, orderState, TriggerTypeEnum.LastPrice, orderType);
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
        public TradeHistoryResponse GetTradeHistoryResponse()
        {
            var request_prep = new TradeHistoryRequest(category, limit);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            TradeHistoryResponse response_obj = null;
            Objects.Responses.TradeHistoryResponse response_unt = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleTradeHistoryResponse(response);
                response_unt = new Objects.Responses.TradeHistoryResponse(response_obj.Result.ResultTotalSize,
                    response_obj.Result.Cursor, response_obj.Result.DataList);
                    

                return response_unt;
            }
            catch (Exception ex)    
            {
                return null;
            }
            return null;

        }
        public WalletInfoResponse GetWalletInfoResponse()
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
                response_unt = new Objects.Responses.WalletInfoResponse(response_obj.RetCode, response_obj.RetMsg,
                    response_obj.ExtCode, response_obj.ExtInfo, response_obj.Result);

                return response_unt;
            }
            catch (Exception ex)    
            {
                return null;
            }
            return null;

        }
        public QueryMyPositionsResponse GetMyPositionsResponse()
        {
            var request_prep = new QueryMyPositionsRequest(category);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryMyPositionsResponse response_obj = null;
            Objects.Responses.MyPositionsResponse response_unt = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryMyPositionsResponse(response);
                response_unt = new Objects.Responses.MyPositionsResponse(response_obj.Result.ResultTotalSize,
                    response_obj.Result.Cursor, response_obj.Result.DataList);

                return response_unt;
            }
            catch (Exception ex)    
            {
                return null;
            }
            return null;

        }
        
        
        /// <summary>
        /// Mapper to internal type
        /// </summary>
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
    }
}