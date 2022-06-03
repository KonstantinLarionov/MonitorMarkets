using System.ComponentModel;
using MonitorMarkets.Application.Abstraction;

namespace MonitorMarkets.Vizualizer.ViewModel
{
    public class CurrencyRateVm : INotifyPropertyChanged
    {
        private readonly FactoryClientService _factory;

        public CurrencyRateVm()
        {
            
            _factory = FactoryClientService.GetInstance();
            _factory.
        }
    }
}