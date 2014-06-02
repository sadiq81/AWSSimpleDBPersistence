using System;
using System.Collections.Generic;

namespace SimpleDBPersistence.SimpleDB.Model.Parameters
{
	[System.Xml.Serialization.XmlTypeAttribute (Namespace = "http://sdb.amazonaws.com/doc/2009-04-15/")]
	[System.Xml.Serialization.XmlRootAttribute (Namespace = "http://sdb.amazonaws.com/doc/2009-04-15/")]
	public class ListDomainsResult
	{
		[System.Xml.Serialization.XmlElementAttribute ("DomainName")]
		public string[] DomainName { get; set; }

		public string NextToken { get; set; }
	}
}

