using System;

namespace AWSSimpleDBPersistence
{
	public class SimpleDBDomainAttribute : Attribute
	{
		public string Domain { get; set; }

		public SimpleDBDomainAttribute (string domain)
		{
			this.Domain = domain;
		}
	}
}

