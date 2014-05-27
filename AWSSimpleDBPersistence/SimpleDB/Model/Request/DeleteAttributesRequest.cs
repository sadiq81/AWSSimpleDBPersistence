using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
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

