using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class CreateDomainRequest : DomainRequest
	{
		public CreateDomainRequest ()
		{
		}

		public CreateDomainRequest (string domainName) : base (domainName)
		{
		}
	}
}

