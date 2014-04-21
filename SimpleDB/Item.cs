using System;
using System.Collections.Generic;
using System.Dynamic;

namespace AWSSimpleDBPersistence
{
	public class Item
	{
		public string ItemName { get; set; }

		public List<ReplaceableAttribute> Attributes { get; set; }

		public Item ()
		{
		}

		public Item (string itemName, List<ReplaceableAttribute> attributes)
		{
			this.ItemName = itemName;
			this.Attributes = attributes;
		}
	}
}

