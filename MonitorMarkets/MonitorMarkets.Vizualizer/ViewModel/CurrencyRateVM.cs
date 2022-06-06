using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using MonitorMarkets.Application.Abstraction;
using MonitorMarkets.Application.Objects.Data;
using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.Vizualizer.ViewModel
{
    public class CurrencyRateVm : INotifyPropertyChanged
    {
        public ICollectionView ListContract { get; set; }
        private readonly FactoryClientService _factory;

        public CurrencyRateVm()
        {
            
            _factory = FactoryClientService.GetInstance();
            var factoryClient=_factory.GetMarket(MarketsEnum.Bybit);
            var listContract = new List<ContractInfoData>();
            foreach (var item in factoryClient.GetContractInfo().Result)
            {
                listContract.Add(item);
            }
            ListContract = CollectionViewSource.GetDefaultView(listContract);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
}