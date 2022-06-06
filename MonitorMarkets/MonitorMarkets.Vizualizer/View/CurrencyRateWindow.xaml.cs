using System.Windows;
using System.Windows.Controls;
using MonitorMarkets.Vizualizer.ViewModel;

namespace MonitorMarkets.Vizualizer.View
{
    public partial class CurrencyRateWindow : Window
    {
        public CurrencyRateWindow()
        {
            InitializeComponent();
            DataContext = new CurrencyRateVm();
        }
    }
}