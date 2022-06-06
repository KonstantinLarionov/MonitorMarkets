using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MonitorMarkets.Application.Abstraction;
using MonitorMarkets.Application.Objects.Data;
using MonitorMarkets.Application.Objects.Data.Enums;
using MonitorMarkets.Application.Objects.Responses;

namespace MonitorMarkets.Vizualizer.ViewModel
{
    public class CurrencyRateVm : INotifyPropertyChanged
    {
        public ICollectionView ListContract { get; set; }
        private readonly FactoryClientService _factory;

        public CurrencyRateVm()
        {
            var listSymbol = new List<string>();

            _factory = FactoryClientService.GetInstance();
            var factoryClientByBit=_factory.GetMarket(MarketsEnum.Bybit);
            var listContract = new List<ContractInfoData>();
            foreach (var item in factoryClientByBit.GetContractInfo().Result)
            {
                listContract.Add(item);
                d_contractInfo.Add(item.Symbol, item);
                listSymbol.Add(item.Symbol);
            }
            
            ListContract = CollectionViewSource.GetDefaultView(listContract);
            
            var factoryClientBitget=_factory.GetMarket(MarketsEnum.Bitget);
            foreach (var item in factoryClientBitget.GetContractInfo().Result)
            {
                listContract.Add(item);
                d_contractInfo.Add(item.Symbol, item);
                listSymbol.Add(item.Symbol);
            }
            ListContract = CollectionViewSource.GetDefaultView(listContract);
            /*foreach (var item in d_contractInfo)
            {
                ComboBox.DataContextProperty = d_contractInfo.ToList();
            }*/
            ListSymbol = CollectionViewSource.GetDefaultView(listSymbol);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        IDictionary<string, ContractInfoData> d_contractInfo =
            new Dictionary<string, ContractInfoData>();

        public ICollectionView ListSymbol { get; set; }

        
        #region Button

        public string _selectedItemInFilter;
        public string SelectedItemInFilter
        {
            get
            {
                return _selectedItemInFilter;
            }
            set
            {
                if (_selectedItemInFilter != value)
                {
                    _selectedItemInFilter = value;
                    ComboBoxChanged();
                }
            }
        }
        private void ComboBoxChanged()
        {
        }


        #endregion

    }
}