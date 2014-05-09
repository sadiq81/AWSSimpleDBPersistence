using System;

namespace AWSSimpleDBPersistence
{
	[System.Xml.Serialization.XmlTypeAttribute (Namespace = "http://sdb.amazonaws.com/doc/2009-04-15/")]
	[System.Xml.Serialization.XmlRootAttribute (Namespace = "http://sdb.amazonaws.com/doc/2009-04-15/")]
	public class SelectResult
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute ("Item")]
		public Item[] Item { get; set; }

		/// <remarks/>
		public string NextToken{ get; set; }

		public SelectResult ()
		{
		}
	}
}

