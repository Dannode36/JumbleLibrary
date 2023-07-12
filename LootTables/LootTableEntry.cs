namespace JumbleLibrary.LootTables
{
    public class LootTableEntry<T>
    {
        public T item;
        public int weight;

        public LootTableEntry(T item, int weight)
        {
            this.item = item;
            this.weight = weight;
        }
    }
}
