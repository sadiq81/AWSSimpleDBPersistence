using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class DomainMetadataRequest : DomainRequest
	{
		public DomainMetadataRequest (string domainName) : base (domainName)
		{
		}
	}
}

