using System;
using System.Web.UI;
using BitgetMapper;
using BitgetMapper.Futures.RestAPI;
using BitgetMapper.Futures.RestAPI.Requests.Account;
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
        private readonly FuturesHanlderComposition _hanlderComposition;
        internal FuturesHanlderComposition hanlderComposition => _hanlderComposition;
        internal void SetUrl(string rest_url)
        {
            _restClient = new RestClient(rest_url);
        }

        internal delegate void Log_Dlg(string sender, string message);

        internal Log_Dlg Log;
        internal bool LogResponceEnabled = false;
        internal bool LogExEnabled = false;

        void OnLogResponce(string response)
        {
            if (LogResponceEnabled)
            {
                Log?.Invoke("RestClient", string.Concat("Response: ", response));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void OnLogEx(Exception ex, string responce = null)
        {
            if (LogExEnabled)
            {
                Log?.Invoke("RestClient",
                    string.Concat("Exception: ", ex.Message, "; ", ex?.InnerException, " - ", responce));
            }
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
        
        internal bool CancelOrderRequest(string symbol, string marginCoin, string orderId)
        {
            var request = _requestArranger.Arrange(new CancelOrderRequest(symbol, marginCoin, orderId));

            string response = string.Empty;
            try
            {
                response = SendRestRequest(request);
                OnLogResponce(response);
                return true;
                
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