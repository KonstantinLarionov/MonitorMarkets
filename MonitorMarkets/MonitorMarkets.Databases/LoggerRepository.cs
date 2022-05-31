using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Databases.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MonitorMarkets.Databases
{
    public class LoggerRepository : IRepository
    {
        LoggerContext _context;
        DbSet<Log> _dbSet;

        public void Create(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        
    }
}
