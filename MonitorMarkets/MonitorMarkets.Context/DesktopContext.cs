using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Context.Entities;

namespace MonitorMarkets.Context;

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