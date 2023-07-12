using System;
using System.Text;
using System.Threading.Tasks;

namespace JumbleLibrary.LootTables
{
    public class ReadonlyLootTable<T> : ILootTable<T>
    {
        public readonly string TableID;
        private readonly List<LootTableEntry<T>> table;
        private readonly int totalWeight;
        private readonly uint tableBits = 0;
        private readonly Random random;

        public ReadonlyLootTable(string tableID, IEnumerable<LootTableEntry<T>> newTable)
        {
            TableID = tableID;
            table = (List<LootTableEntry<T>>)newTable;
            random = new Random();

            foreach (var item in table)
            {
                totalWeight += item.weight;
            }
            foreach (var entry in table)
            {
                tableBits <<= 1;
                tableBits ^= 1;
                tableBits <<= entry.weight - 1;
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

        public T? BinaryLookup()
        {
            uint tableBits = this.tableBits;
            uint randomMask = 0xFFFFFFFF << random.Next(0, totalWeight); //Shift randomly to create a "cutting" bit mask
            
            tableBits &= randomMask; //Fuckery ensues...

            tableBits -= ((tableBits >> 1) & 0x55555555); //Next 3 lines find the total number of set bits in the freshly trimmed table bits
            tableBits = (tableBits & 0x33333333) + ((tableBits >> 2) & 0x33333333); 
            uint index = (((tableBits + (tableBits >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;

            return table[(int)index - 1].item; //Subtracting 1 just makes it work 
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
