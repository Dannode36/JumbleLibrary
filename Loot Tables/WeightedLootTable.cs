using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumbleLibrary.LootTables
{
    public class WeightedTable<T>
    {
        public int totalTrueWeight;
        public readonly string TableID;
        private List<WeightedTableEntry<T>> table;

        public WeightedTable(string tableID, List<WeightedTableEntry<T>> newTable)
        {
            TableID = tableID;
            table = newTable;

            foreach (var item in table)
            {
                totalTrueWeight += item.weight;
            }
        }

        public T GetRandom(int luck = 0)
        {
            int totalWeight = 0;
            foreach (var item in table)
            {
                totalWeight += item.weight + (int)(item.rarity * luck);
            }

            int rand = new Random().Next(0, totalWeight);

            for (int i = 0; i < table.Count; i++)
            {
                totalWeight -= table[i].weight + (int)(table[i].rarity * luck);
                if (totalWeight <= rand)
                {
                    return table[i].item;
                }
            }
            Console.WriteLine($"Somthing went wrong. Rand was {rand}, total weight was {totalWeight}");

            return default(T);
        }

        public T GetRandom(float randomNum, int luck = 0)
        {
            int totalWeight = 0;
            foreach (var item in table)
            {
                totalWeight += item.weight + (item.rarity * luck);
            }

            for (int i = 0; i < table.Count; i++)
            {
                totalWeight -= table[i].weight + (table[i].rarity * luck);
                Console.WriteLine(totalWeight <= randomNum);
                if (totalWeight <= randomNum)
                {
                    return table[i].item;
                }
            }
            Console.WriteLine($"Somthing went wrong. Rand was {randomNum}, total weight was {totalWeight}");

            return default(T);
        }

        //Wtf is this
        public Dictionary<T, int> Loot(int amount, int luck = 0)
        {
            int totalWeight = 0;
            foreach (var item in table)
            {
                totalWeight += item.weight + (item.rarity * luck);
            }

            Dictionary<T, int> loot = new();

            for (int x = 0; x < amount; x++)
            {
                int rand = new Random().Next(0, totalWeight);

                int j = totalWeight;
                for (int i = 0; i < table.Count; i++)
                {
                    j -= table[i].weight + (table[i].rarity * luck);
                    if (j < rand)
                    {
                        if (!loot.TryAdd(table[i].item, 1))
                        {
                            loot[table[i].item]++;
                        }
                        break;
                    }
                }
                if (j > rand)
                {
                    Console.WriteLine($"Somthing went wrong. Rand was {rand}, total weight was {totalWeight}, j was {j}");
                }
            }

            return loot;
        }
    }
}
