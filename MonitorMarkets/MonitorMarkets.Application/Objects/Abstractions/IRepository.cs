using System;
using System.Collections.Generic;

namespace MonitorMarkets.Application.Objects.Abstractions
{
    public interface IRepository<T> 
    {
        int Create(T item);
        T FindById(string id);
        int Remove(T item);
        int Update(T item);
    }
}