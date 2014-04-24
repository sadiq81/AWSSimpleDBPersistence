using System;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class Item
	{
		public string ItemName{ get; set; }

		public List<Attribute> Attributes { get; set; }

		public Item (string itemName)
		{
			this.ItemName = itemName;
		}

		public Item (string itemName, List<Attribute> attributes)
		{
			this.ItemName = itemName;
			this.Attributes = attributes;
		}
	}
}

