using System.Collections.Generic;
using MonitorMarkets.Application.Objects.Data;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class ContractInfoResponse
    {
        public ContractInfoResponse(string retCode, string retMsg, IReadOnlyList<ContractInfoData> result)
        {
            RetCode = retCode;
            RetMsg = retMsg;
            Result = result;
        }
        public string RetCode { get; }
        public string RetMsg { get; }
        public IReadOnlyList<ContractInfoData> Result { get; }
    }
}