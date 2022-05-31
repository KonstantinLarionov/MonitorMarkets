using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Databases.Entities;

namespace MonitorMarkets.Databases
{
    public class LoggerContext : DbContext
    {
        public DbSet<Log> EUsers { get; set; }
        public DbSet<OrdersEntities> EOrders { get; set; }
        public DbSet<PositionsEntities> EPositions { get; set; }
        public DbSet<WalletEntities> EWallet { get; set; }

        public LoggerContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=12345678;database=monitormarketsdb;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }

    }
}