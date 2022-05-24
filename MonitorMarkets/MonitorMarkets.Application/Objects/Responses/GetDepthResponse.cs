using System.Collections.Generic;
using BitgetMapper.Futures.RestAPI.Data.DTO.Market;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class GetDepthResponse
    {
        public GetDepthResponse(string code, GetDepthData data, string msg, long requestTime)
        {
            Code = code;
            Data = data;
            Msg = msg;
            RequestTime = requestTime;

        }
        
        public string Code  { get; }
        public GetDepthData Data { get; set; }
        public string Msg { get; }
        public long RequestTime { get; }

    }
}