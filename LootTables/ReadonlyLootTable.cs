using System.Text;
using System.Threading.Tasks;

namespace JumbleLibrary.LootTables
{
    public class ReadonlyLootTable<T> : ILootTable<T>
    {
        public readonly string TableID;
        private readonly List<LootTableEntry<T>> table;
        private int totalWeight;

        public ReadonlyLootTable(string tableID, IEnumerable<LootTableEntry<T>> newTable)
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
            int tempTotalWeight = totalWeight;

            for (int i = 0; i < table.Count; i++)
            {
                tempTotalWeight -= table[i].weight;
                if (tempTotalWeight <= randomNum)
                {
                    return table[i].item;
                }
            }

            throw new LootTableException("Loot table failed to calculate a returned item", TableID);
        }

        void ILootTable<T>.Add(LootTableEntry<T> entry)
        {
            throw new NotSupportedException("Cannot modify the contents of a ReadonlyLootTable");
        }
        bool ILootTable<T>.Remove(LootTableEntry<T> entry)
        {
            throw new NotSupportedException("Cannot modify the contents of a ReadonlyLootTable");
        }
        bool ILootTable<T>.Remove(T item)
        {
            throw new NotSupportedException("Cannot modify the contents of a ReadonlyLootTable");
        }
    }
}
