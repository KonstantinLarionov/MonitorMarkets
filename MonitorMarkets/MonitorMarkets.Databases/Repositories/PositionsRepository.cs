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

        public void Create(PositionsEntitiesInfo item)
        {
            var itemDb = new PositionsEntities()
            {
                Amount = item.Amount, Price = item.Price, StatusPosition = item.StatusPosition, Symbol = item.Symbol
            };
            _db.Add(itemDb);
            _db.SaveChanges();
        }

        public PositionsEntitiesInfo FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Update(PositionsEntitiesInfo item, Guid id)
        {
            throw new NotImplementedException();
        }

        public PositionsEntitiesInfo FindById(string id)
        {
            var item = _dbSet.Find(id);
            var positionsInfo = new PositionsEntitiesInfo()
            {
                Price = item.Price, Amount = item.Amount, StatusPosition = item.StatusPosition, Symbol = item.Symbol
            };
            return positionsInfo;
        }
        public void Update(PositionsEntitiesInfo item, string id)
        {
            var itemDb = new PositionsEntities()
            { Amount = item.Amount, Price = item.Price, StatusPosition = item.StatusPosition, Symbol = item.Symbol
            };
            _db.Entry(itemDb).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(PositionsEntitiesInfo item)
        {
            var itemDb = new PositionsEntities()
            { Amount = item.Amount, Price = item.Price, StatusPosition = item.StatusPosition, Symbol = item.Symbol
            };
            _db.Remove(itemDb);
            _db.SaveChanges();
        }

        int IRepository<PositionsEntitiesInfo>.Create(PositionsEntitiesInfo item)
        {
            throw new NotImplementedException();
        }
    }
}