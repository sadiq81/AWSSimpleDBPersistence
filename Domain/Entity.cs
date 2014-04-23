using System;

namespace AWSSimpleDBPersistence
{
	public abstract class Entity
	{
		public long Id { get; set; }

		[SimpleDBFieldAttribute ("created")]
		public DateTime Created{ get; set; }

		[SimpleDBFieldAttribute ("updated")]
		public DateTime LastUpdated{ get; set; }
	}
}

