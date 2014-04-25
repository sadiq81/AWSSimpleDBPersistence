using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class BatchDeleteAttributesRequest : DomainRequest
	{
		public List<Item> Items { get; set; }

		public BatchDeleteAttributesRequest ()
		{
		}

		public BatchDeleteAttributesRequest (string domainName, List<Item> items) : base (domainName)
		{
			this.Items = items;
		}
	}
}

