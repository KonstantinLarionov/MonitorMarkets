using System.Collections.Generic;
using MonitorMarkets.Application.Objects.Data;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class CancelOrderResponse
    {
        public CancelOrderResponse(string code, string msg, CancelOrderData data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }
        public string Code { get; }
        public string Msg { get; }
        public CancelOrderData Data { get; }
    }
}