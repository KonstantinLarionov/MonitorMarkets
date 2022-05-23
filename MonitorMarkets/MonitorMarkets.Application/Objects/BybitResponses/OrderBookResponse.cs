using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Market;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class OrderBookResponse
    {
        public OrderBookResponse(int retCode, string retMsg, IReadOnlyList<OrderBookData> result)
        {
            RetCode = retCode;
            RetMsg = retMsg;
            Result = result;
        }
        public int RetCode { get; set; }
        public string RetMsg { get; set; }
        public IReadOnlyList<OrderBookData> Result { get; set; }

    }
}