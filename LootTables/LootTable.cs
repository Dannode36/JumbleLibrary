using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumbleLibrary.LootTables
{
    public class LootTable<T> : ILootTable<T>
    {
        public string TableID;
        private readonly List<LootTableEntry<T>> table;
        private int totalWeight;

        public LootTable(string tableID, IEnumerable<LootTableEntry<T>> newTable)
        {
            TableID = tableID;
            table = (List<LootTableEntry<T>>)newTable;

            foreach (var item in table)
            {
                totalWeight += item.weight;
            }
        }

        public T? Loot()
        {
            int randomNum = new Random().Next(0, totalWeight + 1);
            for (int i = 0; i < table.Count; i++)
            {
                totalWeight -= table[i].weight;
                if (totalWeight <= randomNum)
                {
                    return table[i].item;
                }
            }

            throw new LootTableException("Loot table failed to calculate a returned item", TableID);
        }

        public void Add(LootTableEntry<T> entry)
        {
            table.Add(entry);
            totalWeight += entry.weight;
        }
        public bool Remove(LootTableEntry<T> entry)
        {
            if (table.Remove(entry))
            {
                totalWeight -= entry.weight;
                return true;
            }
            return false;
        }

        // Might require overloading of Equals for this to work propely as "==" can't be applied to generics
        public bool Remove(T item)
        {
            table.RemoveAll(entry => entry.item.Equals(item));
            throw new NotImplementedException();
        }
    }
}
