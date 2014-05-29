using System;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public interface IMarshaller
	{
		IEnumerable<KeyValuePair<string, string>> MarshallPost ();

		string GenerateSignature ();

		void Configure (DomainRequest request);
	}
}

