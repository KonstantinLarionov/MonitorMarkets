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

        private DbSet<WalletEntities> _dbSet;
        public WalletRepository(LoggerContext context)
        {
            _db = context;
            _dbSet = context.Set<WalletEntities>();
        }

        public int Create(WalletEntitiesInfo item)
        {
            var itemDb = new WalletEntities{ Currency = item.Currency, Aviailable = item.Aviailable, Balance = item.Balance};
            _db.Add(itemDb);
            return _db.SaveChanges();
        }

        public int Remove(Guid id)
        {
            var itemDb = _dbSet.Find(id);
            _db.Remove(itemDb);
            return _db.SaveChanges();
        }

        public int Update(WalletEntitiesInfo item, Guid id)
        {
            var itemDb = new WalletEntities{ Id = id, Currency = item.Currency, Aviailable = item.Aviailable, Balance = item.Balance };
            _db.Entry(itemDb).State = EntityState.Modified;
            return _db.SaveChanges();
        }

        public WalletEntitiesInfo FindById(Guid id)
        {
            var item = _dbSet.Find(id);
            var walletInfo = new WalletEntitiesInfo() { Currency = item.Currency, Aviailable = item.Aviailable, Balance = item.Balance };
            return walletInfo;
        }
        
    }
}