using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumbleLibrary.LootTables
{
    public interface ILootTable<T>
    {
        T? Loot();
        void Add(LootTableEntry<T> entry);
        bool Remove(LootTableEntry<T> entry);
        bool Remove(T item);
    }
}
