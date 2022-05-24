using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Account.Account;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class WalletInfoResponse
    {
        public WalletInfoResponse(int retCode, string retMsg, string extCode, string extInfo,
            WalletInfoData result)
        {
            RetCode = retCode;
            RetMsg = retMsg;
            ExtCode = extCode;
            ExtInfo = extInfo;
            Result = result;
        }
        public int RetCode { get; }
        public string RetMsg { get; }
        public string ExtCode { get; }
        public string ExtInfo { get; }
        public WalletInfoData Result { get; set; }

    }
}