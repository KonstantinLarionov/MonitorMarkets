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

        private DbSet<OrdersEntities> _dbSet;
        public OrderRepository(LoggerContext context)
        {
            _db = context;
            _dbSet = context.Set<OrdersEntities>();
        }

        /*public void Create(OrdersEntitiesInfo item)
        {
            var itemDb = new OrdersEntities()
            {
                 Amount = item.Amount, Direction = item.Direction, Price = item.Price,
                StatusOrder = item.StatusOrder
            };

            _db.Add(itemDb);
            _db.SaveChanges();
        }*/

        public OrdersEntitiesInfo FindById(Guid id)
        {
            var item = _dbSet.Find(id);
            var orderInfo = new OrdersEntitiesInfo{ Price = item.Price, Amount = item.Amount, Direction = item.Direction, StatusOrder = item.StatusOrder};
            return orderInfo;
        }
        
        public int Update(OrdersEntitiesInfo item, Guid id)
        {
            var itemDb = new OrdersEntities{ Id = id, Price = item.Price, Amount = item.Amount, Direction = item.Direction, StatusOrder = item.StatusOrder};
            _db.Entry(itemDb).State = EntityState.Modified;
            return _db.SaveChanges();
        }
        
        public int Remove(Guid id)
        {
            var itemDb = _dbSet.Find(id);
            _db.Remove(itemDb);
            return _db.SaveChanges();
        }

        public int Create(OrdersEntitiesInfo item)
        {
            var itemDb = new OrdersEntities { Amount = item.Amount, Direction = item.Direction, Price = item.Price, StatusOrder = item.StatusOrder};
            _db.Add(itemDb);
            return _db.SaveChanges();
        }
    }
}