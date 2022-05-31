using System;
using System.Collections.Generic;

namespace MonitorMarkets.Application.Objects.Abstractions
{
    public interface IRepository<T>
    {
        void Create(T item);
        T FindById(int id);
        void Remove(T item);
        void Update(T item);
    }
}