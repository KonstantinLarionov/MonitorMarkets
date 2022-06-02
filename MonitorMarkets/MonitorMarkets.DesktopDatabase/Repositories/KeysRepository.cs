using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.DesktopDatabase.Entities;

namespace MonitorMarkets.DesktopDatabase.Repositories;

public class KeysRepository:IRepository<ConnectionKeys>
{
    DesktopContext _context;
    DbSet<ConnectionKeys> _dbSet;

    public void Create(ConnectionKeys item)
    {
        _dbSet.Add(item);
        _context.SaveChanges();
    }

    public ConnectionKeys FindById(int id)
    {
        return _dbSet.Find(id);
    }
    public void Update(ConnectionKeys item)
    {
        _context.Entry(item).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Remove(ConnectionKeys item)
    {
        _dbSet.Remove(item);
        _context.SaveChanges();
    }
}