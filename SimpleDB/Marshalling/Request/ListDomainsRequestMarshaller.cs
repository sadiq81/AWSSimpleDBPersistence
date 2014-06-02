using SimpleDBPersistence.SimpleDB.Request;
using System;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Request
{
	public class ListDomainsRequestMarshaller : BaseMarshaller
	{
		public  void Configure (ListDomainsRequest request)
		{

			Arguments.Add ("SignatureMethod", SignatureMethod);
			Arguments.Add ("SignatureVersion", SignatureVersion);
			Arguments.Add ("Timestamp", DateTime.UtcNow.ToString ("o"));
			Arguments.Add ("Version", Version);

			Action = "ListDomains";

			if (request != null && request.NextToken != null) {
				Arguments.Add ("NextToken", request.NextToken);
			}

			if (request != null && request.MaxNumberOfDomains != null) {
				Arguments.Add ("MaxNumberOfDomains", request.MaxNumberOfDomains);
			}
		}
	}
}

