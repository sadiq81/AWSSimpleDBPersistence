using System.Collections.Generic;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using Attribute = SimpleDBPersistence.SimpleDB.Model.Parameters.Attribute;

namespace SimpleDBPersistence.SimpleDB.Request
{
	public class DeleteAttributesRequest : DomainRequest
	{
		public string ItemName { get; set; }

		public List<Attribute> Attributes { get; set; }

		public Expected Expected { get; set; }

		public DeleteAttributesRequest ()
		{
		}


		public DeleteAttributesRequest (string domainName) : base (domainName)
		{
		}

		public DeleteAttributesRequest (string domainName, string itemName) : base (domainName)
		{
			this.ItemName = itemName;
		}
		
	}
}

