
using System.Windows;
using System.Windows.Controls;
using MonitorMarkets.Application.Abstraction;
using MonitorMarkets.Application.Objects.Data.Enums;
using MonitorMarkets.Vizualizer.View;
using MonitorMarkets.Vizualizer.View.Resources.MenuLibrary;
using Menu = MonitorMarkets.Vizualizer.View.Resources.Menu;

namespace MonitorMarkets.Vizualizer
{        

    //
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow
    {
       /* private readonly FactoryClientService _factory;
        public MainWindow()
        {
            _factory = FactoryClientService.GetInstance();
            _factory.GetMarket(MarketsEnum.Binance);
            InitializeComponent();
        }*/


        private void ButtonBase_Settings(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in System.Windows.Application.Current.Windows)
            {
                if (w is GeneralSettingsWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
                
            {
                GeneralSettingsWindow settings = new GeneralSettingsWindow();
                settings.Owner = this;
                settings.Show();
            }
        }

        private void ButtonBase_Trading(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in System.Windows.Application.Current.Windows)
            {
                if (w is TradingWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
                
            {
                TradingWindow trade = new TradingWindow();
                trade.Owner = this;
                trade.Show();
            }
        }

        private void ButtonBase_Charts(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;

            foreach (Window w in System.Windows.Application.Current.Windows)
            {
                if (w is ChartsOfExchangesWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
                
            {
                ChartsOfExchangesWindow currency = new ChartsOfExchangesWindow();
                currency.Owner = this;
                currency.Show();
            }
        }

        private void ButtonBase_Currency(object sender, RoutedEventArgs e)
        { 
            bool isWindowOpen = false;

            foreach (Window w in System.Windows.Application.Current.Windows)
            {
                if (w is CurrencyRateWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
                
            {
                CurrencyRateWindow currency = new CurrencyRateWindow();
                currency.Owner = this;
                currency.Show();
            }
        }
    }
}