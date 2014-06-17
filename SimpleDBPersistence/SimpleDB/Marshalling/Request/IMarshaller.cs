using System.Collections.Generic;
using SimpleDBPersistence.SimpleDB.Request;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Request
{
	public interface IMarshaller
	{
		IEnumerable<KeyValuePair<string, string>> MarshallPost ();

		string GenerateSignature ();

		void Configure (DomainRequest request);
	}
}

