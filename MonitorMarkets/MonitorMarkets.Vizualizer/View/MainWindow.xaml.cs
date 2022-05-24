
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
        private Menu menu;
        public MainWindow()
        {
            InitializeComponent();
            this.menu = new Menu();
            var example = new ExampleWindow();
            example.Show();
        }

        public void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            /*mainSpace.Children.Clear();
            var button = (Button) sender;
            switch (button.Name)
            {
                case  "courses" :
                    mainSpace.Children.Add(menu);
                    break;
            }*/
        }
    }
}