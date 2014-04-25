using System;
using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class DeleteDomainRequest : DomainRequest
	{
		public DeleteDomainRequest ()
		{

		}

		public DeleteDomainRequest (string domainName) : base (domainName)
		{
		}
	}
}

