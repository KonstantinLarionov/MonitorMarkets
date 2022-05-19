using System;
using BitgetMapper.Futures.RestAPI;
using BitgetMapper.Futures.RestAPI.Data.DTO.Enum;
using BitGetMapper.Futures.RestAPI.Data.DTO.Enum;
using BitgetMapper.Futures.RestAPI.Requests.Account;
using BitgetMapper.Futures.RestAPI.Requests.Market;
using BitgetMapper.Futures.RestAPI.Responses.Account;
using BitGetMapper.Futures.RestAPI.Responses.Account;
using BitgetMapper.Futures.RestAPI.Responses.Market;
using BitgetMapper.Requests;
using RestSharp;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    public class BitgetClient
    {
        readonly RequestArranger _requestArranger;
        private FuturesHanlderComposition _composition;
        private RestClient _restClient;
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
        
        #region [Requests]

        #region [Market]
        
        public GetAllSymbolsResponse GetAllSymbolsRequest(ProductTypeEnum productTypeEnum)
        {
            var allSymbols = new GetAllSymbolsRequest(productTypeEnum);
            var request = _requestArranger.Arrange(allSymbols);
            GetAllSymbolsResponse responseObj = null;
            string response = string.Empty;
                try
                {
                    response = SendRestRequest(request);
                    responseObj = _composition.HandleGetAllSymbolsResponse(response);
                    return responseObj;
                }
                catch (Exception ex)
                {
                    OnLogEx(ex,response);
                }
                return responseObj;
        }

        public GetCandleDataResponse GetCandleDataRequest (string symbol, GranularityEnum granularity, DateTime start, DateTime end)
         { 
             var candleData = new GetCandleDataRequest(symbol, granularity, start, end);
            var request = _requestArranger.Arrange(candleData);
            GetCandleDataResponse responseObj = null;
            string response = string.Empty;
                try
                {
                    response = SendRestRequest(request);
                    responseObj = _composition.HandleGetCandleDataResponse(response);
                    return responseObj;
                }
                catch (Exception ex)
                {
                   OnLogEx(ex, response);
                }
                return responseObj;
        }

        public GetDepthResponse GetDepthRequest(string symbol)
        {
            var depth = new GetDepthRequest(symbol);
            var request = _requestArranger.Arrange(depth);
            GetDepthResponse responseObj = null;
            string response = string.Empty;
                try
                {
                    response = SendRestRequest(request);
                    responseObj = _composition.HandleGetDepthResponse(response);
                    return responseObj;
                }
                catch (Exception ex)
                {
                    OnLogEx(ex, response);
                }
                return responseObj ;
        }
        
        public ServerTimeResponse ServerTimeRequest()
        {
            var serverTime = new ServerTimeRequest();
            var request = _requestArranger.Arrange(serverTime);
            ServerTimeResponse responseObj = null;
            string response = string.Empty;
                try
                {
                    response = SendRestRequest(request);
                    responseObj = _composition.HandleServerTimeResponse(response);
                    return responseObj;
                }
                catch (Exception ex)
                {
                    OnLogEx(ex, response);
                }
                return responseObj ;
        }
        
        #endregion
        
        #region [Account]
        public CancelOrderResponse CancelOrderRequest(string symbol, string marginCoin, string orderId)
        {
            var cancelOrder = new CancelOrderRequest(symbol, marginCoin, orderId);
            var request = _requestArranger.Arrange(cancelOrder);
            CancelOrderResponse responseObj = null;
            string response = string.Empty;
                try
                {
                    response = SendRestRequest(request);
                    responseObj = _composition.HandleCancelOrderResponse(response);
                    return responseObj;
                    
                }
                catch (Exception ex)
                {
                    OnLogEx(ex, response);
                }
                return responseObj;
        }
        
        public GetHistoryOrderResponse GetHistoryOrderRequest(string symbol, DateTime startTime, DateTime endTime, string pageSize)
        {
            var historyOrder = new GetHistoryOrderRequest(symbol, startTime, endTime, pageSize);
            var request = _requestArranger.Arrange(historyOrder);
            GetHistoryOrderResponse responseObj = null;
            string response = string.Empty;
                try
                {
                    response = SendRestRequest(request);
                    responseObj = _composition.HandleGetHistoryOrderResponse(response);
                    return responseObj;
                    
                }
                catch (Exception ex)
                {
                    OnLogEx(ex, response);
                }
                return responseObj;
        }
        
        public GetOpenOrderResponse GetOpenOrderRequest(string symbol)
        {
            var openOrder = new GetOpenOrderRequest(symbol);
            var request = _requestArranger.Arrange(openOrder);
            GetOpenOrderResponse responseObj = null;
            string response = string.Empty;
                try
                {
                    response = SendRestRequest(request);
                    responseObj = _composition.HandleGetOpenOrderResponse(response);
                    return responseObj;
                    
                }
                catch (Exception ex)
                {
                    OnLogEx(ex, response);
                }
                return responseObj;
        }
       
        public GetSingleAccountResponse GetSingleAccountRequest(string symbol, string marginCoin)
        {
            var singleAccount = new GetSingleAccountRequest(symbol, marginCoin);
            var request = _requestArranger.Arrange(singleAccount);
            GetSingleAccountResponse responseObj = null;
            string response = string.Empty;
                try
                {
                    response = SendRestRequest(request);
                    responseObj = _composition.HandleGetSingleAccountResponse(response);
                    return responseObj;
                    
                }
                catch (Exception ex)
                {
                    OnLogEx(ex, response);
                }
                return responseObj;
        }
        
        public PlaceOrderResponse PlaceOrderRequest(string symbol, string marginCoin, decimal size,
            SideType side, OrderTypeEnum orderType)
        {
            var placeOrder = new PlaceOrderRequest(symbol, marginCoin, size, side, orderType);
            var request = _requestArranger.Arrange(placeOrder);
            PlaceOrderResponse responseObj = null;
            string response = string.Empty;
                try
                {
                    response = SendRestRequest(request);
                    responseObj = _composition.HandlePlaceOrderResponse(response);
                    return responseObj;
                    
                }
                catch (Exception ex)
                {
                    OnLogEx(ex, response);
                }
                return responseObj;
        }
        
        #endregion
        
        #endregion
    }
}