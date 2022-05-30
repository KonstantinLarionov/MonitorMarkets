using System.Data.Entity;
using MonitorMarkets.Database.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore; 

namespace MonitorMarkets.Database
{
    public class LoggerContext : DbContext
    {
        
        public DbSet<Log> Users { get; set; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"MonitorDB.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }
    }
}