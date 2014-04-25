using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class PutAttributesRequest : DomainRequest
	{
		public string ItemName { get; set; }

		public List<ReplaceableAttribute> ReplaceableAttributes { get; set; }

		public Expected Expected { get; set; }

		public PutAttributesRequest () : base ()
		{
		}

		public PutAttributesRequest (string domainName) : base (domainName)
		{
		}

		public PutAttributesRequest (string domainName, string itemName) : base (domainName)
		{
			this.ItemName = itemName;
		}

		public PutAttributesRequest (string domainName, string itemName, List<ReplaceableAttribute> replaceableAttributes) : base (domainName)
		{
			this.ItemName = itemName;
			this.ReplaceableAttributes = replaceableAttributes;
		}

		public PutAttributesRequest (string domainName, string itemName, List<ReplaceableAttribute> replaceableAttributes, Expected expected) : base (domainName)
		{
			this.ItemName = itemName;
			this.ReplaceableAttributes = replaceableAttributes;
			this.Expected = expected;
		}
	}
}

