using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class PutAttributesRequest : Request
	{
		public string DomainName { get; set; }

		public Item Item { get; set; }

		public UpdateCondition Expected { get; set; }
	}
}

