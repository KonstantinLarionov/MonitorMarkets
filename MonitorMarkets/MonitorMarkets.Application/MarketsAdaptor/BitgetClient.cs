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
using MonitorMarkets.Application.Abstraction;
using MonitorMarkets.Application.Objects.Data;
using MonitorMarkets.Application.Objects.Data.Enums;
using MonitorMarkets.Application.Objects.Responses;
using RestSharp;
using CancelOrderResponse = BitgetMapper.Futures.RestAPI.Responses.Account.CancelOrderResponse;
using GetAllSymbolsResponse = BitgetMapper.Futures.RestAPI.Responses.Market.GetAllSymbolsResponse;
using GetDepthResponse = BitgetMapper.Futures.RestAPI.Responses.Market.GetDepthResponse;
using GetHistoryOrderResponse = BitGetMapper.Futures.RestAPI.Responses.Account.GetHistoryOrderResponse;
using GetOpenOrderResponse = BitGetMapper.Futures.RestAPI.Responses.Account.GetOpenOrderResponse;
using GetSingleAccountResponse = BitgetMapper.Futures.RestAPI.Responses.Account.GetSingleAccountResponse;
using OrderTypeEnum = BitGetMapper.Futures.RestAPI.Data.DTO.Enum.OrderTypeEnum;
using PlaceOrderResponse = BitgetMapper.Futures.RestAPI.Responses.Account.PlaceOrderResponse;
using WebSocketSharp;

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

        public OrderBookResponse GetOrderBookResponse()
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

        public TradeHistoryResponse GetTradeHistoryResponse()
        {
            throw new NotImplementedException();
        }

        public WalletInfoResponse GetWalletInfoResponse()
        {
            throw new NotImplementedException();
        }

        public Objects.Responses.KlineResponse GetCandleDataRequest(string symbol, GranularityEnum granularity,
            DateTime start, DateTime end)
        {
            var candleData = new GetCandleDataRequest(symbol, granularity, start, end);
            var request = _requestArranger.Arrange(candleData);
            List<List<Decimal>> response_obj = null;
            string response = string.Empty;
            Objects.Responses.KlineResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetCandleDataResponse(response);

                foreach (var item in response_obj)
                {
                    var time = Convert.ToInt64(item[0]);
                    response_unt = new Objects.Responses.KlineResponse(time, item[1],item[2],item[3],item[4],item[5]);
                    return response_unt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
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
        
        public Objects.Responses.CancelOrderResponse GetCancelOrder(string symbol, string marginCoin, string orderid)
        {
            var cancelOrder = new CancelOrderRequest(symbol, marginCoin, orderid);
            var request = _requestArranger.Arrange(cancelOrder);
            string response = string.Empty;
            CancelOrderResponse response_obj = null;
            Objects.Responses.CancelOrderResponse response_unt = null;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleCancelOrderResponse(response);
                response_unt = new Objects.Responses.CancelOrderResponse(response_obj.Data.OrderId);
                
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
        public Objects.Responses.WalletInfoResponse GetSingleAccountRequest(string symbol, string marginCoin)
        {
            var singleAccount = new GetSingleAccountRequest(symbol, marginCoin);
            var request = _requestArranger.Arrange(singleAccount);
            GetSingleAccountResponse response_obj = null;
            Objects.Responses.WalletInfoResponse response_unt = null;

            string response = string.Empty;

            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetSingleAccountResponse(response);
                response_unt = new Objects.Responses.WalletInfoResponse("USDT", response_obj.Data.UsdtEquity, response_obj.Data.Available);
                return response_unt;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
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
                
                response_unt = new Objects.Responses.PlaceOrderResponse(response_obj.Data.OrderId, 0, 0, OrderActionEnum.Unknown, OrderMarkerEnum.Unknown);

                return response_unt;

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
        /*public Objects.Responses.OrderBookResponse GetOrderBookResponse()
        {
        }*/
        public Objects.Responses.UnfilledResponse GetUnfilledResponse(string symbol)
        {
            var placeOrder = new GetOpenOrderRequest(symbol);
            var request = _requestArranger.Arrange(placeOrder);
            GetOpenOrderResponse response_obj = null;
            string response = string.Empty;
            Objects.Responses.UnfilledResponse response_unt = null;
                                                                        
            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetOpenOrderResponse(response);
                
                response_unt = new Objects.Responses.UnfilledResponse();

                return response_unt;

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;

        }
        public Objects.Responses.OrderHistoryResponse GetOrderHistoryRequest(string symbol, DateTime startTime, DateTime endTime, string pageSize)
        {
            var placeOrder = new GetHistoryOrderRequest(symbol, startTime, endTime, pageSize);
            var request = _requestArranger.Arrange(placeOrder);
            GetHistoryOrderResponse response_obj = null;
            string response = string.Empty;
            Objects.Responses.OrderHistoryResponse response_unt = null;
                                                                        
            try
            {
                response = SendRestRequest(request);
                response_obj = _composition.HandleGetHistoryOrderResponse(response);

                foreach (var item in response_obj.Data.OrderList)
                {
                    response_unt = new Objects.Responses.OrderHistoryResponse(item.Symbol, item.OrderId, OrderActionEnum.Unknown, item.Price, item.Size, item.FilledQty, item.Ctime, Objects.Data.Enums.OrderTypeEnum.Unknown, TriggerTypeEnum.Unknown, OrderStateEnum.None);
                    return response_unt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;

        }
        /*public Objects.Responses.TradeHistoryResponse GetTradeHistoryResponse()
        {
        }*/
        public Objects.Responses.MyPositionsResponse GetMyPositionsResponse()
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

        #endregion

        #endregion

        #region WebSocket

        WebSocket m_WebSocketPublic = null;
        WebSocket m_WebSocketPrivate = null;
        /*public StartSocket()
        {
            throw new NotImplementedException();
        }

        public StopSocket()
        {
            throw new NotImplementedException();
        }

        private StartSocketPrivate()
        {
            throw new NotImplementedException();
        }

        private StopSocketPrivate()
        {
            
        }

        public Connect()
        {
            
        }

        private ConnectPrivate()
        {
            
        }
        */
        
        #endregion

    }
}