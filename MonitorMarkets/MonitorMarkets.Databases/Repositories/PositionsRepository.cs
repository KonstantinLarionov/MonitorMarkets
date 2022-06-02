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

        private DbSet<PositionsEntitiesInfo> _dbSet;
        public PositionsRepository(LoggerContext context)
        {
            _db = context;
            _dbSet = context.Set<PositionsEntitiesInfo>();
        }

        public void Create(PositionsEntitiesInfo item)
        {
            var itemDb = new PositionsEntities()
            {
                Id = Guid.NewGuid().ToString(), Amount = item.Amount, Price = item.Price, StatusPosition = item.StatusPosition, Symbol = item.Symbol
            };
            _db.Add(itemDb);
            _db.SaveChanges();
        }

        public PositionsEntitiesInfo FindById(string id)
        {
            return _dbSet.Find(id);
        }
        public void Update(PositionsEntitiesInfo item)
        {
            var itemDb = new PositionsEntities()
            {
                Id = Guid.NewGuid().ToString(), Amount = item.Amount, Price = item.Price, StatusPosition = item.StatusPosition, Symbol = item.Symbol
            };
            _db.Entry(itemDb).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(PositionsEntitiesInfo item)
        {
            var itemDb = new PositionsEntities()
            {
                Id = Guid.NewGuid().ToString(), Amount = item.Amount, Price = item.Price, StatusPosition = item.StatusPosition, Symbol = item.Symbol
            };
            _db.Remove(itemDb);
            _db.SaveChanges();
        }
        
    }
}