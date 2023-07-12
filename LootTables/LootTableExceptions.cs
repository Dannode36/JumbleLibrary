using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumbleLibrary.LootTables
{
	[Serializable]
	public class LootTableException : Exception
	{
        public string? LootTableID { get; }
        public LootTableException() { }
		public LootTableException(string message) : base(message) { }
        public LootTableException(string message, string lootTableID) : base(message)
		{
            LootTableID = lootTableID;
        }
        public LootTableException(string message, Exception inner) : base(message, inner) { }
		protected LootTableException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
