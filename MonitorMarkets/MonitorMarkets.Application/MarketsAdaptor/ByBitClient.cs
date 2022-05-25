using System;
using System.Collections.Generic;
using System.Globalization;
using BybitMapper.Requests;
using BybitMapper.UsdcPerpetual.RestV2;
using BybitMapper.UsdcPerpetual.RestV2.Data.Enums;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Account.Account;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Account.Order;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Account.Positions;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Account.Wallet;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Market;
using BybitMapper.UsdcPerpetual.RestV2.Responses.Market;
using MonitorMarkets.Application.Objects.Data;
using MonitorMarkets.Application.Objects.Responses;
using RestSharp;
using CancelOrderResponse = BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order.CancelOrderResponse;
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

        #region [Account]
        public Objects.Responses.CancelOrderResponse GetCancelOrder(string symbol)
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
                response_unt = new Objects.Responses.CancelOrderResponse(response_obj.RetCode, response_obj.RetMsg,
                    new CancelOrderData(response_obj.Result.OrderId));

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public Objects.Responses.PlaceOrderResponse PlaceOrderRequest(string symbol, OrderType orderType, OrderFilterType orderFilter,
            SideType side, decimal orderQty)
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
                response_unt = new Objects.Responses.PlaceOrderResponse(response_obj.Result.OrderId);

                return response_unt;

            }
            catch (Exception ex)    
            {
                return null;
            }

            return null;
        }

        public Objects.Responses.QueryOrderHistoryResponse QueryOrderHistoryRequest(CategoryType category, long time)
        {
            var request_prep = new QueryOrderHistoryRequest(category);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryOrderHistoryResponse response_obj = null;
            Objects.Responses.QueryOrderHistoryResponse response_unt = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryOrderHistoryResponse(response);
                response_unt = new Objects.Responses.QueryOrderHistoryResponse(response_obj.Result.ResultTotalSize, response_obj.Result.Cursor, response_obj.Result.DataList);

                return response_unt;
            }
            catch (Exception ex)    
            {
                return null;
            }

            return null;
        }

        public Objects.Responses.QueryUnfilledResponse QueryUnfilledRequest(CategoryType category)
        {
            var request_prep = new QueryUnfilledRequest(category);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryUnfilledResponse response_obj = null;
            Objects.Responses.QueryUnfilledResponse response_unt = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryUnfilledResponse(response);
                response_unt = new Objects.Responses.QueryUnfilledResponse(response_obj.Result.ResultTotalSize,
                    response_obj.Result.Cursor, response_obj.Result.DataList);

                return response_unt;
            }
            catch (Exception ex)    
            {
                return null;
            }
            return null;
        }

        public Objects.Responses.TradeHistoryResponse TradeHistoryRequest(CategoryType category, DateTime startTime, int limit)
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

        public Objects.Responses.QueryMyPositionsResponse QueryMyPositionsRequest(CategoryType category)
        {
            var request_prep = new QueryMyPositionsRequest(category);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryMyPositionsResponse response_obj = null;
            Objects.Responses.QueryMyPositionsResponse response_unt = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryMyPositionsResponse(response);
                response_unt = new Objects.Responses.QueryMyPositionsResponse(response_obj.Result.ResultTotalSize,
                    response_obj.Result.Cursor, response_obj.Result.DataList);

                return response_unt;
            }
            catch (Exception ex)    
            {
                return null;
            }
            return null;
        }

        public Objects.Responses.WalletInfoResponse WalletInfoRequest()
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
        
        #endregion

        #region [Market]

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

        public Objects.Responses.OrderBookResponse OrderBookRequest(string symbol)
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
                response_unt = new Objects.Responses.OrderBookResponse(response_obj.RetCode, response_obj.RetMsg, response_obj.Result);

                return response_unt;
            }
            catch (Exception ex)    
            {
                return null;
                
            }

            return null;
        }

        public Objects.Responses.QueryKlineResponse QueryKlineRequest(string symbol, IntervalType period, DateTime startTime)
        {
            var request_prep = new QueryKlineRequest(symbol, period, startTime);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryKlineResponse response_obj = null;
            Objects.Responses.QueryKlineResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryKlineResponse(response);
                response_unt = new Objects.Responses.QueryKlineResponse(response_obj.RetCode, response_obj.RetMsg,
                    response_obj.Result);

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

        
    }
}