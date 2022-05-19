using System;
using System.Net;
using BybitMapper.InversePerpetual.RestV2.Requests.Market;
using BybitMapper.Perpetual.RestV2.Data.Enums;
using BybitMapper.Perpetual.RestV2.Responses.Account.Position;
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
        internal UsdcPepetualHandlerComposition HandlerComposition => m_HandlerComposition;
        RequestArranger m_RequestArranger;

        internal void UsdcPerpetualRestClient(RequestArranger ra)
        {
            m_HandlerComposition = new UsdcPepetualHandlerComposition(new UsdcPepetualHandlerFactory());
            m_RequestArranger = ra;
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
            { Log?.Invoke("RestClient", string.Concat("Response: ", response)); }
        }
        /// <summary>
        /// 
        /// </summary>
        void OnLogEx(Exception ex, string response = null)
        {
            if (LogExEnabled)
            { Log?.Invoke("RestClient", string.Concat("Exception: ", ex.Message, "; ", ex?.InnerException, " - ", response)); }
        }


        #endregion
        
        #region [Request]

        #region [Account]
        public CancelOrderResponse CancelOrderRequest(string symbol, OrderFilterType orderFilter)
        {
            var request_prep = new CancelOrderRequest(symbol, orderFilter);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            CancelOrderResponse response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandleCancelOrderResponse(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                OnLogEx(ex,response);
            }

            return response_obj;
        }

        public PlaceOrderResponse PlaceOrderRequest(string symbol, OrderType orderType, OrderFilterType orderFilter,
            SideType side, decimal orderQty)
        {
            var request_prep = new PlaceOrderRequest(symbol, orderType, orderFilter, side, orderQty);
            var request = m_RequestArranger.Arrange(request_prep);
            string response = string.Empty;
            PlaceOrderResponse response_obj = null;
            
            try
            {
                response = SendRestRequest(request);
                response_obj = m_HandlerComposition.HandlePlaceOrderResponse(response);

                return response_obj;
            }
            catch (Exception ex)    
            {
                OnLogEx(ex,response);
            }

            return response_obj;
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
                OnLogEx(ex,response);
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
                OnLogEx(ex,response);
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
                OnLogEx(ex,response);
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
                OnLogEx(ex,response);
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
                OnLogEx(ex,response);
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
                OnLogEx(ex,response);
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
                OnLogEx(ex,response);
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
                OnLogEx(ex,response);
            }

            return response_obj;
        }

        #endregion
        
        #endregion
    }
}