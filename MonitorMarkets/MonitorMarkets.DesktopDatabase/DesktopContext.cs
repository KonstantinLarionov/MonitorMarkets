using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MonitorMarkets.DesktopDatabase.Repositories;

namespace MonitorMarkets.DesktopDatabase;
using Microsoft.EntityFrameworkCore;
using Entities;

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