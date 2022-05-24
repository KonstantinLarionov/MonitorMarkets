using System.Collections.Generic;
using BitGetMapper.Futures.RestAPI.Data.DTO;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class GetOpenOrderResponse
    {
        public GetOpenOrderResponse(string code, IReadOnlyList<GetOpenOrderData> data, string msg, long requestTime)
        {
            Code = code;
            Data = data;
            Msg = msg;
            RequestTime = requestTime;
        }

        public IReadOnlyList<GetOpenOrderData> Data { get; set; }
        public string Code { get; set; }
        public long RequestTime { get; set; }
        public string Msg { get; set; }

    }
}