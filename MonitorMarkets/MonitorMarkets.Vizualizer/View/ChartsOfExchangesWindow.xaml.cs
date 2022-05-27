using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using MonitorMarkets.Application.MarketsAdaptor;
using MonitorMarkets.Application.Objects.Data.Enums;

namespace MonitorMarkets.Vizualizer.View
{
    public partial class ChartsOfExchangesWindow : Window
    {
        public ChartsOfExchangesWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_Charts(object sender, RoutedEventArgs e)
        {
            var charts = new ChartsWindow();
            charts.Show();
        }
    }
}