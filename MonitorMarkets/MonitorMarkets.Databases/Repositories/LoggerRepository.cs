using Microsoft.EntityFrameworkCore;
using MonitorMarkets.Application.Objects.Abstractions;
using MonitorMarkets.Databases.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using MonitorMarkets.Application.Objects.DataBase;

namespace MonitorMarkets.Databases.Repositories
{
    public class LoggerRepository : IRepository<LogInfo>
    {
        LoggerContext _db;

        private DbSet<LogInfo> _dbSet;
        public LoggerRepository(LoggerContext context)
        {
            _db = context;
            _dbSet = context.Set<LogInfo>();
        }

        public void Create(LogInfo item)
        {
            _db.Add(item);
            _db.SaveChanges();
        }

        public LogInfo FindById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Update(LogInfo item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(LogInfo item)
        {
            _db.Remove(item);
            _db.SaveChanges();
        }
        
    }
}
