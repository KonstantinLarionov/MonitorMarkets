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
    public class WalletRepository : IRepository<WalletEntitiesInfo>
    {
        LoggerContext _db;

        private DbSet<WalletEntitiesInfo> _dbSet;
        public WalletRepository(LoggerContext context)
        {
            _db = context;
            _dbSet = context.Set<WalletEntitiesInfo>();
        }

        public void Create(WalletEntitiesInfo item)
        {
            _db.Add(item);
            _db.SaveChanges();
        }

        public WalletEntitiesInfo FindById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Update(WalletEntitiesInfo item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(WalletEntitiesInfo item)
        {
            _db.Remove(item);
            _db.SaveChanges();
        }
        
    }
}