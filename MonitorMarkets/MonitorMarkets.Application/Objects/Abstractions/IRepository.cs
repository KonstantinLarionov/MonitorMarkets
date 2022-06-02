using System;
using System.Collections.Generic;

namespace MonitorMarkets.Application.Objects.Abstractions
{
    public interface IRepository<T> 
    {
        int Create(T item);
        T FindById(Guid id);
        int Remove(Guid id);
        int Update(T item, Guid id);
    }
}