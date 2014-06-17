using System.Collections.Generic;
using SimpleDBPersistence.SimpleDB.Model.Parameters;

namespace SimpleDBPersistence.SimpleDB.Request
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

