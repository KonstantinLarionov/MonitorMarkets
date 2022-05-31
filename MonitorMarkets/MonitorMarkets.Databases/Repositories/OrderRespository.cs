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
    public class OrderRepository : IRepository<OrdersEntitiesInfo>
    {
        LoggerContext _db;

        private DbSet<OrdersEntitiesInfo> _dbSet;
        public OrderRepository(LoggerContext context)
        {
            _db = context;
            _dbSet = context.Set<OrdersEntitiesInfo>();
        }

        public void Create(OrdersEntitiesInfo item)
        {
            _db.Add(item);
            _db.SaveChanges();
        }

        public OrdersEntitiesInfo FindById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Update(OrdersEntitiesInfo item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(OrdersEntitiesInfo item)
        {
            _db.Remove(item);
            _db.SaveChanges();
        }
        
    }
}