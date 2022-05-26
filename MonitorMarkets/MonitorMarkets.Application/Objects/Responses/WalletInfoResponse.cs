using System.Collections.Generic;
using BybitMapper.UsdcPerpetual.RestV2.Data.ObjectDTO.Account.Account;

namespace MonitorMarkets.Application.Objects.Responses
{
    public class WalletInfoResponse
    {
        public WalletInfoResponse(string currency, decimal balance, decimal aviailable)
        {
            Currency = currency;
            Balance = balance;
            Aviailable = aviailable;
        }
        public string Currency { get; }
        public decimal Balance { get; }
        public decimal Aviailable { get; }
    }
}