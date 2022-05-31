using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Databases.Entities;

namespace MonitorMarkets.Databases;

public class LoggerRepository
{
    LoggerContext _context;
    DbSet<Log> _dbSet;
    public void Create(Log item)
    {
        _dbSet.Add(item);
        _context.SaveChanges();
    }
    public void Update(Log item)
    {
        _context.Entry(item).State = EntityState.Modified;
        _context.SaveChanges();
    }
    public void Remove(Log item)
    {
        _dbSet.Remove(item);
        _context.SaveChanges();
    }
}
