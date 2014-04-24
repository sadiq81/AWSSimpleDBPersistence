using System;

namespace AWSSimpleDBPersistence
{
	public interface IMarshaller
	{
		string Marshal ();

		string GenerateSignature ();

		void Configure (DomainRequest request);
	}
}

