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
    public class PositionsRepository : IRepository<PositionsEntitiesInfo>
    {
        LoggerContext _db;

        private DbSet<PositionsEntities> _dbSet;
        public PositionsRepository(LoggerContext context)
        {
            _db = context;
            _dbSet = context.Set<PositionsEntities>();
        }
        
        public int Create(PositionsEntitiesInfo item)
        {
            var itemDb = new PositionsEntities{ Symbol = item.Symbol, Price = item.Price, StatusPosition = item.StatusPosition};
            _db.Add(itemDb);
            return _db.SaveChanges();
        }
        public int Remove(Guid id)
        {
            var itemDb = _dbSet.Find(id);
            _db.Remove(itemDb);
            return _db.SaveChanges();
        }
        public int Update(PositionsEntitiesInfo item, Guid id)
        {
            var itemDb = new PositionsEntities{ Id = id, Symbol = item.Symbol, Price = item.Price, Amount = item.Amount, StatusPosition = item.StatusPosition};
            _db.Entry(itemDb).State = EntityState.Modified;
            return _db.SaveChanges();
        }
        public PositionsEntitiesInfo FindById(Guid id)
        {
            var item = _dbSet.Find(id);
            var posInfo = new PositionsEntitiesInfo { Symbol = item.Symbol, Price = item.Price, Amount = item.Amount, StatusPosition = item.StatusPosition };
            return posInfo;
        }
    }
}