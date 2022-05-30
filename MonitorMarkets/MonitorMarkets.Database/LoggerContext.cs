using System.Data.Entity;
using MonitorMarkets.Database.Entities;

namespace MonitorMarkets.Database
{
    public class LoggerContext : DbContext
    {
        public LoggerContext() :base("DefaultConnection")
        {}
        public DbSet<Log> EUsers { get; set; }
        public DbSet<OrdersEntities> EOrders { get; set; }
        public DbSet<PositionsEntities> EPositions { get; set; }
        public DbSet<WalletEntities> EWallet { get; set; }
    }
}