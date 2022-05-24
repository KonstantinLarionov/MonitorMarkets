using System.Collections.Generic;
using BitgetMapper.Futures.RestAPI.Data.DTO.Market;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class GetAllSymbolsResponse
    {
        public GetAllSymbolsResponse(string code, IReadOnlyList<GetAllSymbolsData> data, string msg, string requestTime)
        {
            Code = code;
            Data = data;
            Msg = msg;
            RequestTime = requestTime;
        }
        public string Code { get; set; }
        public IReadOnlyList<GetAllSymbolsData> Data { get; set; }
        public string Msg { get; set; }
        public string RequestTime { get; set; }
    }
}