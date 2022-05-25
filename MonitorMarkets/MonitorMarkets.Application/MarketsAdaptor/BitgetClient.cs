using System;
using System.Collections.Generic;
using BitgetMapper.Futures.RestAPI;
using BitgetMapper.Futures.RestAPI.Data.DTO;
using BitgetMapper.Futures.RestAPI.Data.DTO.Enum;
using BitGetMapper.Futures.RestAPI.Data.DTO.Enum;
using BitgetMapper.Futures.RestAPI.Data.DTO.Market;
using BitgetMapper.Futures.RestAPI.Requests.Account;
using BitgetMapper.Futures.RestAPI.Requests.Market;
using BitgetMapper.Futures.RestAPI.Responses.Account;
using BitGetMapper.Futures.RestAPI.Responses.Account;
using BitgetMapper.Futures.RestAPI.Responses.Market;
using BitgetMapper.Requests;
using MonitorMarkets.Application.Objects.Data;
using MonitorMarkets.Application.Objects.Responses;
using RestSharp;
using CancelOrderResponse = BitgetMapper.Futures.RestAPI.Responses.Account.CancelOrderResponse;
using GetAllSymbolsResponse = BitgetMapper.Futures.RestAPI.Responses.Market.GetAllSymbolsResponse;
using GetCandleDataResponse = MonitorMarkets.Application.Objects.Responses.GetCandleDataResponse;
using GetDepthResponse = BitgetMapper.Futures.RestAPI.Responses.Market.GetDepthResponse;
using GetHistoryOrderResponse = BitGetMapper.Futures.RestAPI.Responses.Account.GetHistoryOrderResponse;
using GetOpenOrderResponse = BitGetMapper.Futures.RestAPI.Responses.Account.GetOpenOrderResponse;
using GetSingleAccountResponse = BitgetMapper.Futures.RestAPI.Responses.Account.GetSingleAccountResponse;
using PlaceOrderResponse = BitgetMapper.Futures.RestAPI.Responses.Account.PlaceOrderResponse;

namespace MonitorMarkets.Application.MarketsAdaptor
{
    public class BitgetClient : IMarketClient
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
                    listData.Add(new ContractInfoData(item.Symbol, null, item.BaseCoin, item.QuoteCoin, item.TakerFeeRate, item.MakerFeeRate, null, null, null, null, null, null,null, null, null, null, null));
                }

                response_unt = new ContractInfoResponse(response_obj.Code, response_obj.Msg, listData);
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;        }

        /// <summary>
        /// CandleData данные о свечах
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="granularity"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>#1 Timestamp,#2 Timestamp,#3 Highest price,#4 Lowest price,#5 Closing price,#6 Base currency trading volume,#7 Quote currency trading volume</returns>
        public Objects.Responses.GetCandleDataResponse GetCandleDataRequest(string symbol, GranularityEnum granularity,
            DateTime start, DateTime end)
        {
            var candleData = new GetCandleDataRequest(symbol, granularity, start, end);
            var request = _requestArranger.Arrange(candleData);
            List<List<Decimal>> response_obj = null;
            string response = string.Empty;
            Objects.Responses.GetCandleDataResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetCandleDataResponse(response);
                response_unt = new Objects.Responses.GetCandleDataResponse(response_unt.Code, response_unt.Data,
                    response_unt.Msg, response_unt.RequestTime);

                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public Objects.Responses.GetDepthResponse GetDepthRequest(string symbol)
        {
            var depth = new GetDepthRequest(symbol);
            var request = _requestArranger.Arrange(depth);
            GetDepthResponse response_obj = null;
            Objects.Responses.GetDepthResponse response_unt = null;

            string response = string.Empty;
            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetDepthResponse(response);
                response_unt = new Objects.Responses.GetDepthResponse(response_obj.Code, response_obj.Data,
                    response_obj.Msg, response_obj.RequestTime);
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="marginCoin"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        
        
        public Objects.Responses.CancelOrderResponse GetCancelOrder(string symbol)
        {
            var cancelOrder = new CancelOrderRequest(symbol, null, null);
            var request = _requestArranger.Arrange(cancelOrder);
            string response = string.Empty;
            CancelOrderResponse response_obj = null;
            Objects.Responses.CancelOrderResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleCancelOrderResponse(response);
                response_unt = new Objects.Responses.CancelOrderResponse(response_obj.Code, response_obj.Msg, new CancelOrderData(response_obj.Data.OrderId));
                
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Objects.Responses.GetHistoryOrderResponse GetHistoryOrderRequest(string symbol, DateTime startTime,
            DateTime endTime, string pageSize)
        {
            var historyOrder = new GetHistoryOrderRequest(symbol, startTime, endTime, pageSize);
            var request = _requestArranger.Arrange(historyOrder);
            GetHistoryOrderResponse response_obj = null;
            string response = string.Empty;
            Objects.Responses.GetHistoryOrderResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetHistoryOrderResponse(response);
                response_unt = new Objects.Responses.GetHistoryOrderResponse(response_obj.Code, response_obj.Data,
                    response_obj.Msg);

                return response_unt;

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>

        public Objects.Responses.GetOpenOrderResponse GetOpenOrderRequest(string symbol)
        {
            var openOrder = new GetOpenOrderRequest(symbol);
            var request = _requestArranger.Arrange(openOrder);
            GetOpenOrderResponse response_obj = null;
            Objects.Responses.GetOpenOrderResponse response_unt = null;

            string response = string.Empty;
            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetOpenOrderResponse(response);
                response_unt = new Objects.Responses.GetOpenOrderResponse(response_obj.Code, response_obj.Data,
                    response_obj.Msg, response_obj.RequestTime);

                return response_unt;

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="marginCoin"></param>
        /// <returns></returns>

        public Objects.Responses.GetSingleAccountResponse GetSingleAccountRequest(string symbol, string marginCoin)
        {
            var singleAccount = new GetSingleAccountRequest(symbol, marginCoin);
            var request = _requestArranger.Arrange(singleAccount);
            GetSingleAccountResponse response_obj = null;
            Objects.Responses.GetSingleAccountResponse response_unt = null;

            string response = string.Empty;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetSingleAccountResponse(response);
                response_unt = new Objects.Responses.GetSingleAccountResponse(response_obj.Code, response_obj.Data,
                    response_obj.Msg, response_obj.RequestTime);
                return response_unt;

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="marginCoin"></param>
        /// <param name="size"></param>
        /// <param name="side"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>

        public Objects.Responses.PlaceOrderResponse PlaceOrderRequest(string symbol, string marginCoin, decimal size,
            SideType side, OrderTypeEnum orderType)
        {
            var placeOrder = new PlaceOrderRequest(symbol, marginCoin, size, side, orderType);
            var request = _requestArranger.Arrange(placeOrder);
            PlaceOrderResponse response_obj = null;
            string response = string.Empty;
            Objects.Responses.PlaceOrderResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandlePlaceOrderResponse(response);
                response_unt = new Objects.Responses.PlaceOrderResponse(response_obj.Data.OrderId);

                return response_unt;

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public OrderBookResponse GetOrderBookResponse()
        {
        }
        public KlineResponse GetKlineResponse()
        {
        }

        public Objects.Responses.PlaceOrderResponse GetPlaceOrderResponse()
        {
        }

        public Objects.Responses.CancelOrderResponse GetCancelOrderResponse()
        {
        }

        public UnfilledResponse GetUnfilledResponse()
        {
        }

        public OrderHistoryResponse GetOrderHistoryResponse()
        {
        }

        public TradeHistoryResponse GetTradeHistoryResponse()
        {
        }

        public WalletInfoResponse GetWalletInfoResponse()
        {
        }

        public MyPositionsResponse GetMyPositionsResponse()
        {
        }

        #endregion

        #endregion

    }
}