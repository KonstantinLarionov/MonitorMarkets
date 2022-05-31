using System;
using System.Data.Entity;
using MonitorMarkets.Contexts.Entities;

namespace MonitorMarkets.Contexts
{
    public class DesktopContext: DbContext
    {
        public DbSet<ConnectionsKeys> ConnectionsKeys { get; set; }

        public DesktopContext() : base("MonitorMarket")
        {
        }
    }
}