using System;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class ListDomainsResult
	{
		public List<String> DomainName { get; set; }

		public string NextToken { get; set; }
	}
}

