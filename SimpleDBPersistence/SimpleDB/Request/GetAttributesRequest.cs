using System.Collections.Generic;

namespace SimpleDBPersistence.SimpleDB.Request
{
	public class GetAttributesRequest : DomainRequest
	{
		public string ItemName { get; set; }

		public List<string> AttributeNames { get; set; }

		public bool ConsistentRead { get; set; }

		public GetAttributesRequest ()
		{
		}

		public GetAttributesRequest (string domainName) : base (domainName)
		{
		}

		public GetAttributesRequest (string domainName, string itemName) : base (domainName)
		{
			this.ItemName = itemName;
		}

		public GetAttributesRequest (string domainName, string itemName, bool consistentRead) : base (domainName)
		{
			this.ItemName = itemName;
			this.ConsistentRead = consistentRead;
		}

		public GetAttributesRequest (string domainName, string itemName, List<string> attributeNames) : base (domainName)
		{
			this.ItemName = itemName;
			this.AttributeNames = attributeNames;
		}

		public GetAttributesRequest (string domainName, string itemName, List<string> attributeNames, bool consistentRead) : base (domainName)
		{
			this.ItemName = itemName;
			this.AttributeNames = attributeNames;
			this.ConsistentRead = consistentRead;
		}
	}
}

