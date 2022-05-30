using System.Data.Entity;
using MonitorMarkets.Database.Entities;

namespace MonitorMarkets.Database
{
    public class LoggerContext : DbContext
    {
        public LoggerContext() :base("DefaultConnection")
        {}
        
        public DbSet<Log> Users { get; set; }
         
    }
}