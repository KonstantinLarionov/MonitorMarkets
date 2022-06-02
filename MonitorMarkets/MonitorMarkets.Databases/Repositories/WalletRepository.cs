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

        public void Create(WalletEntitiesInfo item)
        {
            var itemDb = new WalletEntities()
            { Currency = item.Currency, Balance = item.Balance, Aviailable = item.Aviailable
            };
            _db.Add(itemDb);
            _db.SaveChanges();
        }

        public WalletEntitiesInfo FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Update(WalletEntitiesInfo item, Guid id)
        {
            throw new NotImplementedException();
        }

        public WalletEntitiesInfo FindById(string id)
        {
            var item = _dbSet.Find(id);
            var walletInfo = new WalletEntitiesInfo()
            {
                Currency = item.Currency, Aviailable = item.Aviailable, Balance = item.Balance
            };
            return walletInfo;
        }
        public void Update(WalletEntitiesInfo item, string id)
        {
            var itemDb = new WalletEntities()
            { Currency = item.Currency, Balance = item.Balance, Aviailable = item.Aviailable
            };
            _db.Entry(itemDb).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Remove(WalletEntitiesInfo item)
        {
            var itemDb = new WalletEntities()
            { Currency = item.Currency, Balance = item.Balance, Aviailable = item.Aviailable
            };
            _db.Remove(itemDb);
            _db.SaveChanges();
        }

        int IRepository<WalletEntitiesInfo>.Create(WalletEntitiesInfo item)
        {
            throw new NotImplementedException();
        }
    }
}