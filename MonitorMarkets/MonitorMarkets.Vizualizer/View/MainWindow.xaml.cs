
using System.Windows;
using System.Windows.Controls;

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
        //private readonly FactoryClientService _factory;
        public MainWindow()
        {
            /*_factory = FactoryClientService.GetInstance();
            _factory.GetMarket(MarketsEnum.Binance);*/
            InitializeComponent();
            /*this.menu = new Menu();
            var example = new ExampleWindow();
            example.Show();*/
        }


        private void ButtonBase_Settings(object sender, RoutedEventArgs e)
        {
            int chld = VisualChildrenCount;
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is GeneralSettingsWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                GeneralSettingsWindow newwindow = new GeneralSettingsWindow();
                newwindow.Show();
            }
        }

        private void ButtonBase_Trading(object sender, RoutedEventArgs e)
        {
            int chld = VisualChildrenCount;
            bool isWindowOpen = false;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is GeneralSettingsWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                GeneralSettingsWindow newwindow = new GeneralSettingsWindow();
                newwindow.Show();
            }
        }

        private void ButtonBase_Charts(object sender, RoutedEventArgs e)
        {
            var charts = new ChartsOfExchanges();
            charts.Show();
        }

        private void ButtonBase_Currency(object sender, RoutedEventArgs e)
        {
            var currency = new CurrencyRate();
            currency.Show();
        }
    }
}