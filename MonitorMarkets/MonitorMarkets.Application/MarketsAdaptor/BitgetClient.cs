using System;
using System.Web.UI;
using BitgetMapper;
using BitgetMapper.Futures.RestAPI;
using BitgetMapper.Futures.RestAPI.Requests.Account;
using BitgetMapper.Futures.RestAPI.Responses.Account;
using BitgetMapper.Requests;
using JetBrains.Annotations;
using Org.BouncyCastle.Bcpg.Sig;
using RestSharp;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    public class BitgetClient
    {
        readonly RequestArranger _requestArranger = new RequestArranger();
        private FuturesHanlderComposition _composition = new FuturesHanlderComposition(new FuturesHandlerFactory());
        private RestClient _restClient;
        public BitgetClient(string rest_url)
        {
            _restClient = new RestClient(rest_url);
        }
        
        /// <summary>
        /// 
        /// </summary>
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

        #region [Requests]
        
        public CancelOrderResponse CancelOrderRequest(string symbol, string marginCoin, string orderId)
        {
            new CancelOrderRequest(symbol, marginCoin, orderId)
            var request = _requestArranger.Arrange();

            string response = string.Empty;
            try
            {
                response = SendRestRequest(request);
                var response_obj = _composition.HandleCancelOrderResponse(response);
                
                return response_obj;
                
            }
            catch (Exception ex)
            {
                OnLogEx(ex, response);
            }
            return false;
        }
        
        #endregion
    }
}