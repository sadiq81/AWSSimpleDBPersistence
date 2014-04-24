using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class BatchPutAttributesRequest : DomainRequest
	{
		public List<ReplaceableItem> ReplaceableItems { get; set; }

		public BatchPutAttributesRequest ()
		{
		}

		public BatchPutAttributesRequest (string domainName) : base (domainName)
		{
		}

		public BatchPutAttributesRequest (string domainName, List<ReplaceableItem> replaceableItems) : base (domainName)
		{
			this.ReplaceableItems = replaceableItems;
		}
	}
}

