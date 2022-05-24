using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Market;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class QueryKlineResponse
    {
        public QueryKlineResponse(int retCode, string retMsg, IReadOnlyList<QueryKlineData> result)
        {
            RetCode = retCode;
            RetMsg = retMsg;
            Result = result;
        }
        public int RetCode { get; }
        public string RetMsg { get; }
        public IReadOnlyList<QueryKlineData> Result { get; }
    }
}