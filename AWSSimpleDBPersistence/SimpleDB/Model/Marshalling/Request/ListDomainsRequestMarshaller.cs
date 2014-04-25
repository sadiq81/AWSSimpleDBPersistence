using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Runtime.CompilerServices;

namespace AWSSimpleDBPersistence
{
	public class ListDomainsRequestMarshaller : BaseMarshaller
	{
		public  void Configure ()
		{
			Arguments.Add ("SignatureMethod", SignatureMethod);
			Arguments.Add ("SignatureVersion", SignatureVersion);
			Arguments.Add ("Timestamp", DateTime.UtcNow.ToString ("o"));
			Arguments.Add ("Version", Version);
			this.Action = "ListDomains";

		}
	}
}

