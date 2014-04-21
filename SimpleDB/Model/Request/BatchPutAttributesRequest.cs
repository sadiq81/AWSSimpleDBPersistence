using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class BatchPutAttributesRequest : Request
	{
		public BatchPutAttributesRequest (PutAttributesRequest request)
		{
			this.DomainName = request.DomainName;
			this.Items = new List<Item> ();
			this.Items.Add (request.Item);
		}

		public string DomainName { get; set; }

		public List<Item> Items { get; set; }
	}
}

