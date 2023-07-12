namespace JumbleLibrary.LootTables
{
    public class WeightedTableEntry<T>
    {
        public T item;
        public int weight;
        public int rarity;

        public WeightedTableEntry(T item, int weight, int rarity = 0)
        {
            this.item = item;
            this.weight = weight;
            this.rarity = rarity;
        }
    }
}
