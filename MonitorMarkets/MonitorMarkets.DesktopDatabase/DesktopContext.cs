using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MonitorMarkets.DesktopDatabase.Repositories;

using Microsoft.EntityFrameworkCore;
using MonitorMarkets.DesktopDatabase.Entities;

namespace MonitorMarkets.DesktopDatabase;

public class DesktopContext:DbContext
{
    public DbSet<ConnectionKeys> ConnectionKeys { get; set; }

    public DesktopContext()
    {
        Database.EnsureCreated();
    }

    protected void OnCofiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename = DesktopContext.db");
    }
    
    
}