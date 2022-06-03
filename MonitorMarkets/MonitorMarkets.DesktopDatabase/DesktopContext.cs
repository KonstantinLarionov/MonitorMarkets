using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MonitorMarkets.DesktopDatabase.Repositories;

using Microsoft.EntityFrameworkCore;
using MonitorMarkets.DesktopDatabase.Entities;

namespace MonitorMarkets.DesktopDatabase;

public class DesktopContext:DbContext
{
    //public DbSet<ConnectionKeys> ConnectionKeys { get; set; }

    /*public DesktopContext()
    {
        Database.EnsureCreated();
    }*/
    public DbSet<ConnectionKeys> ConnectionKeys { get; set; }
    public DesktopContext(DbContextOptions<DesktopContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }


}