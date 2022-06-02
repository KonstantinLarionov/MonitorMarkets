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

        private DbSet<Log> _dbSet;
        
        public LoggerRepository(LoggerContext context)
        {
            _db = context;
            _dbSet = context.Set<Log>();
        }

        public void Create(LogInfo item)
        {
            var itemDb = new Log { Id = Guid.NewGuid().ToString(), MsgError = item.MsgError, Time = item.Time, TypeError = item.TypeError };
            _db.Add(itemDb);
            _db.SaveChanges();
        }
        
        public LogInfo FindById(string id)
        {
            var item = _dbSet.Find(id);
            var logInfo = new LogInfo() { MsgError = item.MsgError, Time = item.Time, TypeError = item.TypeError };
            return logInfo;
        }
        public void Update(LogInfo item)
        {
            var itemDb = new Log { Id = Guid.NewGuid().ToString(), MsgError = item.MsgError, Time = item.Time, TypeError = item.TypeError };
            _db.Entry(itemDb).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(LogInfo item)
        {
            var itemDb = new Log { Id = Guid.NewGuid().ToString(), MsgError = item.MsgError, Time = item.Time, TypeError = item.TypeError };
            _db.Remove(itemDb);
            _db.SaveChanges();
        }
        
    }
}
