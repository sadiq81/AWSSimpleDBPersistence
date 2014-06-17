using System;
using SimpleDBPersistence.SimpleDB.Model;

namespace SimpleDBPersistence.Domain
{
	public abstract class Entity
	{
		[SimpleDBIdAttribute]
		public long Id { get; set; }

		[SimpleDBFieldAttribute ("Created")]
		public DateTime Created{ get; set; }

		[SimpleDBFieldAttribute ("Updated")]
		public DateTime LastUpdated{ get; set; }
	}
}

