using System.Windows;

namespace MonitorMarkets.Vizualizer.View
{
    public partial class ChartsOfExchanges : Window
    {
        public ChartsOfExchanges()
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