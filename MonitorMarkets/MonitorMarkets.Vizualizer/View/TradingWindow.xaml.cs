using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;

namespace MonitorMarkets.Vizualizer.View
{
    public partial class TradingWindow : Window
    {
        public TradingWindow()
        {
            InitializeComponent();
        }

        public void Box_OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            ComboBoxItem cbi = ((sender as ComboBox).SelectionBoxItem as ComboBoxItem);
            TextBox.Text = ("   You selected " + cbi.Content.ToString()+ ".");
        }
    }
}