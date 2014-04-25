using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class ListDomainsRequest
	{
		public string MaxNumberOfDomains { get; set; }

		public string NextToken { get; set; }
	}
}

