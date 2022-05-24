using System.Collections.Generic;
using MonitorMarkets.Application.Objects.Data;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class CancelOrderResponse
    {
        public CancelOrderResponse(string retCode, string retMsg, IReadOnlyList<CancelOrderData> result)
        {
            RetCode = retCode;
            RetMsg = retMsg;
            Result = result;
        }
        public string RetCode { get; }
        public string RetMsg { get; }
        public IReadOnlyList<CancelOrderData> Result { get; }
    }
}