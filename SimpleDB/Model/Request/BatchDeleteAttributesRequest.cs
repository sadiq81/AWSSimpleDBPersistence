using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class BatchDeleteAttributesRequest : DomainRequest
	{
		List<Item> Items;

		public BatchDeleteAttributesRequest (string domainName, List<Item> items) : base (domainName)
		{
			this.Items = items;
		}
	}
}

