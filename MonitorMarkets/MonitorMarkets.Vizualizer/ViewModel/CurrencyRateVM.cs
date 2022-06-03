using System.ComponentModel;
using MonitorMarkets.Application.Abstraction;
using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.Vizualizer.ViewModel
{
    public class CurrencyRateVm
    {
        private readonly FactoryClientService _factory;

        public CurrencyRateVm()
        {
            
            _factory = FactoryClientService.GetInstance();
            var factoryClient = _factory.GetMarket(MarketsEnum.Bybit);
        }
    }
}