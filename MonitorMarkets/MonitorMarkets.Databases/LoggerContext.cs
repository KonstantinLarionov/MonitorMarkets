using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Application.Objects.DataBase;
using MonitorMarkets.Databases.Entities;

namespace MonitorMarkets.Databases
{
    public class LoggerContext : DbContext
    {
        public DbSet<LogInfo> EUsers { get; set; }
        public DbSet<OrdersEntitiesInfo> EOrders { get; set; }
        public DbSet<PositionsEntitiesInfo> EPositions { get; set; }
        public DbSet<WalletEntitiesInfo> EWallet { get; set; }

        public LoggerContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=root;database=monitormarketsdb;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
    }
}