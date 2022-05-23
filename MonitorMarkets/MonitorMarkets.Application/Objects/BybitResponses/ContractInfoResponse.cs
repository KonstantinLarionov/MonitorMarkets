using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Market;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class ContractInfoResponse
    {
        public ContractInfoResponse(int retCode, string retMsg, IReadOnlyList<ContractInfoData> result)
        {
            RetCode = retCode;
            RetMsg = retMsg;
            Result = result;
        }
        public int RetCode { get; }
        public string RetMsg { get; }
        public IReadOnlyList<ContractInfoData> Result { get; }
    }
}