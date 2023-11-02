using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumbleLibrary.Inventory
{
    public interface IStorage<T>
    {
        public void Add(T item);
        public void Remove(T item);
        public void Clear();
    }

    public class Inventory<T> : IStorage<T>
    {
        List<T> items;

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
