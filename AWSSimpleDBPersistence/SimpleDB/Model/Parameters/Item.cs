using System;
using System.Collections.Generic;
using Attribute = AWSSimpleDBPersistence.Attribute;

namespace AWSSimpleDBPersistence
{
	[System.Xml.Serialization.XmlTypeAttribute (Namespace = "http://sdb.amazonaws.com/doc/2009-04-15/")]
	[System.Xml.Serialization.XmlRootAttribute (Namespace = "http://sdb.amazonaws.com/doc/2009-04-15/")]
	public class Item
	{
		public string Name { get; set; }

		[System.Xml.Serialization.XmlElementAttribute ("Attribute")]
		public Attribute[] Attribute{ get; set; }

		public Item ()
		{
		}

		public Item (string name)
		{
			this.Name = name;
		}

		public Item (string name, Attribute[] attribute)
		{
			this.Name = name;
			this.Attribute = attribute;
		}
	}
}

