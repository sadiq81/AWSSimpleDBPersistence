using SimpleDBPersistence.SimpleDB.Model.Parameters;
using System.Collections.Generic;

namespace SimpleDBPersistence.SimpleDB.Request
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

