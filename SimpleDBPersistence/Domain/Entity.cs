using System;
using SimpleDBPersistence.SimpleDB.Model;

namespace SimpleDBPersistence.Domain
{
	public abstract class Entity
	{
		[SimpleDBIdAttribute]
		public virtual string Id { get; set; }

		public const string CreatedIdentifier = "Created";

		[SimpleDBFieldAttribute (CreatedIdentifier)]
		public virtual DateTime Created{ get; set; }

		public const string UpdatedIdentifier = "Updated";

		[SimpleDBFieldAttribute (UpdatedIdentifier)]
		public virtual DateTime LastUpdated{ get; set; }

		public override string ToString ()
		{
			return string.Format ("[Entity: Id={0}, Created={1}, LastUpdated={2}]", Id, Created, LastUpdated);
		}
		
	}
}

