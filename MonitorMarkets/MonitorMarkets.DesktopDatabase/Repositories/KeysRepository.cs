using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.DesktopDatabase.Entities;

namespace MonitorMarkets.DesktopDatabase.Repositories;

public class KeysRepository:IRepository<ConnectionKeys>
{
    DesktopContext _db;
    DbSet<ConnectionKeys> _dbSet;
    
    public KeysRepository(DesktopContext context)
    {
        _db = context;
        _dbSet = context.Set<ConnectionKeys>();
    }

    public int Create(ConnectionKeys item)
    {
        _dbSet.Add(item);
        var res = _db.SaveChanges();
        return res;
    }

    public ConnectionKeys FindById(Guid id)
    {
        return _dbSet.Find(id);
    }

    public int Remove(Guid id)
    {
        Remove(id);
        var res = _db.SaveChanges();
        return res;
    }

    public int Update(ConnectionKeys item, Guid id)
    {
        _dbSet.Find(id);
        _db.Entry(item).State = EntityState.Modified;
        var res = _db.SaveChanges();
        return res;
    }


}