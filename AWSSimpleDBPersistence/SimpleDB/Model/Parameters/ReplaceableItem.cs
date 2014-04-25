using System;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class ReplaceableItem
	{
		public string ItemName{ get; set; }

		public List<ReplaceableAttribute> ReplaceableAttributes { get; set; }

		public ReplaceableItem ()
		{
		}

		public ReplaceableItem (Entity entity)
		{
			this.ItemName = string.Format ("{0}", entity.Id);
		}

		public ReplaceableItem (string itemName)
		{
			this.ItemName = itemName;
		}

		public ReplaceableItem (string itemName, List<ReplaceableAttribute> replaceableAttributes)
		{
			this.ItemName = itemName;
			this.ReplaceableAttributes = replaceableAttributes;
		}
	}
}

