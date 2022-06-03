using System;
using System.Windows;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Contexts;
using MonitorMarkets.Contexts.Entities;

namespace MonitorMarkets.Vizualizer.View
{
    public partial class GeneralSettingsWindow : Window
    {
        // private readonly IRepository<ConnectionKeys>
        public GeneralSettingsWindow()
        {
            /*DesktopContext db = new DesktopContext();
            db.ConnectionsKeys.Add(new ConnectionsKeys()
                { DateTime = DateTime.Now, PassPhrase = "123", PublicKeys = "321", SecretKey = "54353" });
            db.SaveChanges();
            */
            
            InitializeComponent();
        }
    }
}