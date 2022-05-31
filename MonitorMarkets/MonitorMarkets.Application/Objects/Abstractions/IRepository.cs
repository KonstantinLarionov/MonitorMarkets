using System;
using System.Collections.Generic;

namespace MonitorMarkets.Application.Objects.Abstractions
{
    public interface IRepository<T> 
    {
        void Create(T item);
        T FindById(string id);
        void Remove(T item);
        void Update(T item);
    }
}