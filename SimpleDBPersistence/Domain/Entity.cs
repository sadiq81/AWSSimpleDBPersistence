using System;
using SimpleDBPersistence.SimpleDB.Model;

namespace SimpleDBPersistence.Domain
{
	public abstract class Entity
	{
		[SimpleDBIdAttribute]
		public string Id { get; set; }

		[SimpleDBFieldAttribute ("Created")]
		public DateTime Created{ get; set; }

		[SimpleDBFieldAttribute ("Updated")]
		public DateTime LastUpdated{ get; set; }

		public override string ToString ()
		{
			return string.Format ("[Entity: Id={0}, Created={1}, LastUpdated={2}]", Id, Created, LastUpdated);
		}
		
	}
}

