using System;
using BybitMapper.Requests;
using BybitMapper.UsdcPerpetual.RestV2;
using BybitMapper.UsdcPerpetual.RestV2.Data.Enums;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Account.Order;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Account.Positions;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Account.Wallet;
using BybitMapper.UsdcPerpetual.RestV2.Requests.Market;
using BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Account;
using BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Order;
using BybitMapper.UsdcPerpetual.RestV2.Responses.Account.Positions;
using BybitMapper.UsdcPerpetual.RestV2.Responses.Market;
using RestSharp;
using IntervalType = BybitMapper.UsdcPerpetual.RestV2.Data.Enums.IntervalType;
using OrderBookRequest = BybitMapper.UsdcPerpetual.RestV2.Requests.Market.OrderBookRequest;
using OrderType = BybitMapper.UsdcPerpetual.RestV2.Data.Enums.OrderType;
using SideType = BybitMapper.UsdcPerpetual.RestV2.Data.Enums.SideType;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    internal class ByBitClient
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
        public Objects.Responses.CancelOrderResponse CancelOrderRequest(string symbol, OrderFilterType orderFilter)
        {
            var request_prep = new CancelOrderRequest(symbol, orderFilter);
            var request = m_RequestArranger.Arrange(request_prep);
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
                response_unt = new Objects.Responses.PlaceOrderResponse(response_obj.Result.Symbol, response_obj.Result.);

                return response_unt;

            }
            catch (Exception ex)    
            {
                return null;
            }

            return null;
        }

        public QueryOrderHistoryResponse QueryOrderHistoryRequest(CategoryType category, long time)
        {
            var request_prep = new QueryOrderHistoryRequest(category, time);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryOrderHistoryResponse response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryOrderHistoryResponse(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                return null;
            }

            return response_obj;

        }

        public QueryUnfilledResponse QueryUnfilledRequest(CategoryType category)
        {
            var request_prep = new QueryUnfilledRequest(category);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryUnfilledResponse response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryUnfilledResponse(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                return null;
            }
            return response_obj;
        }

        public TradeHistoryResponse TradeHistoryRequest(CategoryType category, DateTime startTime, int limit)
        {
            var request_prep = new TradeHistoryRequest(category, startTime, limit);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            TradeHistoryResponse response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleTradeHistoryResponse(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                return null;
            }
            return response_obj;
        }

        public QueryMyPositionsResponse QueryMyPositionsRequest(CategoryType category)
        {
            var request_prep = new QueryMyPositionsRequest(category);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryMyPositionsResponse response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryMyPositionsResponse(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                return null;
            }
            return response_obj;
        }

        public WalletInfoResponse WalletInfoRequest()
        {
            var request_prep = new WalletInfoRequest();
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            WalletInfoResponse response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleWalletInfoResponse(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                return null;
            }
            return response_obj;
        }
        
        #endregion

        #region [Market]

        public ContractInfoResponce ContractInfoRequest()
        {
            var request_prep = new ContractInfoRequest();
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            ContractInfoResponce response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleContractInfoResponce(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                return null;
            }

            return response_obj;
        }

        public OrderBookResponse OrderBookRequest(string symbol)
        {
            var request_prep = new OrderBookRequest(symbol);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            OrderBookResponse response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleOrderBookResponse(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                return null;
                
            }

            return response_obj;
        }

        public QueryKlineResponse QueryKlineRequest(string symbol, IntervalType period, DateTime startTime)
        {
            var request_prep = new QueryKlineRequest(symbol, period, startTime);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            QueryKlineResponse response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleQueryKlineResponse(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                return null;
                
            }

            return response_obj;
        }

        #endregion
        
        #endregion
    }
}