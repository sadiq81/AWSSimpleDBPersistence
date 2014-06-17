using Attribute = SimpleDBPersistence.SimpleDB.Model.Parameters.Attribute;

namespace SimpleDBPersistence.SimpleDB.Model.Parameters
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

