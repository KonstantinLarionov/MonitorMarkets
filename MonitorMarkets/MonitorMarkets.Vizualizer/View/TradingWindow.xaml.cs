using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MonitorMarkets.Application.Objects.Data.Attributes;
using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.Vizualizer.View
{
    public partial class TradingWindow : Window
    {
        MarketsEnum status;
        public TradingWindow()
        {
            InitializeComponent();
        }

        public void Box_OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            //var enumtype = SelectedMarket.Text;
            //MarketsEnum markets = (MarketsEnum) Enum.Parse(typeof(MarketsEnum), (string) SelectedMarket.SelectedValue);
            /*enumMarket= (MarketsEnum) Enum.Parse(typeof(MarketsEnum), SelectedMarket.SelectedItem);
            MarketBar.Text = (market);
            var qwe = enumtype.GetType().FullName;*/
            //var marketBinance = enumMarket.GetEnumName(MarketsEnum.Binance);
            Enum.TryParse<MarketsEnum>(SelectedMarket.SelectedValue.ToString(), out status);
        }

        private  void PrintAuthorInfo(System.Type type )
        {
            // Using reflection.  
            var attrs = status.GetCustomAttributes(); // Reflection.  

            // Displaying output.  
            foreach (Attribute attr in attrs)
            {
                if (attr is EnumMarketsAttribute)
                {
                    EnumMarketsAttribute a = (EnumMarketsAttribute) attr;
                }
            }
        }
    }
}