using System;

namespace SimpleDBPersistence.SimpleDB.Model
{
	public class SimpleDBDomainAttribute : System.Attribute
	{
		public string Domain { get; set; }

		public SimpleDBDomainAttribute (string domain)
		{
			this.Domain = domain;
		}
	}
}

