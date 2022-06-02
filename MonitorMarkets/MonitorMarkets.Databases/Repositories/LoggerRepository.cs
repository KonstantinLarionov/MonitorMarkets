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

        public int Create(LogInfo item)
        {
            var itemDb = new Log { MsgError = item.MsgError, TypeError = item.TypeError };
            _db.Add(itemDb);
            return _db.SaveChanges();
        }
        
        public LogInfo FindById(Guid id)
        {
            var item = _dbSet.Find(id);
            var logInfo = new LogInfo() { MsgError = item.MsgError,  TypeError = item.TypeError };
            return logInfo;
        }
        public int Update(LogInfo item, Guid id)
        {
            var itemDb = new Log { Id = id, MsgError = item.MsgError,  TypeError = item.TypeError };
            _db.Entry(itemDb).State = EntityState.Modified;
            return _db.SaveChanges();
        }

        public int Remove(Guid id)
        {
            var itemDb = _dbSet.Find(id);
            _db.Remove(itemDb);
            return _db.SaveChanges();
        }
        
    }
}
