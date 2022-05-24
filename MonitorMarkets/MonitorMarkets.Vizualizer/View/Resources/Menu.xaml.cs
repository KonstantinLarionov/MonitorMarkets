using System.Windows;
using System.Windows.Controls;
using MonitorMarkets.Vizualizer.View.Resources.MenuLibrary;

namespace MonitorMarkets.Vizualizer.View.Resources
{
    public partial class Menu : UserControl
    {
        private Coins _coins;
        public Menu()
        {
            InitializeComponent();
            this._coins = new Coins();
        }

        public void Button_Table(object sender, RoutedEventArgs e)
        {
            ListCoins.Children.Clear();
            var button = (Button) sender;
            switch (button.Name)
            {
                case  "coins" :
                    ListCoins.Children.Add(_coins);
                    break;
            }
        }
    }
}
