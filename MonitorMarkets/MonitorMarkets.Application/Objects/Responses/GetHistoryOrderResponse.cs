using System.Collections.Generic;
using BitGetMapper.Futures.RestAPI.Data.DTO;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class GetHistoryOrderResponse
    {
        public GetHistoryOrderResponse(string code, GetHistoryData data, string msg)
        {
            Code = code;
            Data = data;
            Msg = msg;
        }   
        public GetHistoryData Data { get; set; }
        public string Code { get; set; }
        public string Msg { get; set; }
    }
}