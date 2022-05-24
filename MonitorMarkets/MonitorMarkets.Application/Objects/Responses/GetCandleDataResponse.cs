using System.Collections.Generic;
using BitgetMapper.Futures.RestAPI.Data.DTO;
using BitgetMapper.Futures.RestAPI.Data.DTO.Market;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class GetCandleDataResponse
    {
        public GetCandleDataResponse(string code, List<GetCandleData> data, string msg,
            string requestTime)
        {
            Code = code;
            Data = data;
            Msg = msg;
            RequestTime = requestTime;
        }
        public string Code  { get; }
        public List<GetCandleData> Data { get; }
        public string Msg { get; }
        public string RequestTime { get; }

    }
}