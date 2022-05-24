using System.Collections.Generic;
using BitgetMapper.Futures.RestAPI.Responses.Account;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class GetSingleAccountResponse
    {
        public GetSingleAccountResponse(string code, GetSingleAccountData data, string msg,
            long requestTime)
        {
            Code = code;
            Data = data;
            Msg = msg;
            RequestTime = requestTime;
        }
        public string Code { get; }
        public GetSingleAccountData Data { get; }
        public string Msg { get; }
        public long RequestTime { get; }
    }
}