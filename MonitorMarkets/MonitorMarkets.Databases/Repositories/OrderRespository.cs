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
            var itemDb = new OrdersEntities()
            {
                Id = Guid.NewGuid().ToString(), Amount = item.Amount, Direction = item.Direction, Price = item.Price,
                StatusOrder = item.StatusOrder
            };

            _db.Add(itemDb);
            _db.SaveChanges();
        }

        public OrdersEntitiesInfo FindById(string id)
        {
            return _dbSet.Find(id);
        }
        public void Update(OrdersEntitiesInfo item)
        {
            var itemDb = new OrdersEntities()
            {
                Id = Guid.NewGuid().ToString(), Amount = item.Amount, Direction = item.Direction, Price = item.Price,
                StatusOrder = item.StatusOrder
            };

            _db.Entry(itemDb).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(OrdersEntitiesInfo item)
        {
            var itemDb = new OrdersEntities()
            {
                Id = Guid.NewGuid().ToString(), Amount = item.Amount, Direction = item.Direction, Price = item.Price,
                StatusOrder = item.StatusOrder
            };

            _db.Remove(itemDb);
            _db.SaveChanges();
        }
        
    }
}