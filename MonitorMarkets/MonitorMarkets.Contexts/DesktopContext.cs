using System;
using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Contexts.Entities;

namespace MonitorMarkets.Contexts
{
    public class DesktopContext:DbContext
    {
        public DbSet<ConnectionsKeys> ConnectionsKeys { get; set; }

        public DesktopContext()
        {
            Database.EnsureCreated();
        }

        protected void OnCofiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename = DesktopContext.db");
        }
    }
}