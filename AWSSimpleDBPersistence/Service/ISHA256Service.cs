using System;

namespace AWSSimpleDBPersistence
{
	public interface ISHA256Service
	{
		string CreateHash (string message, string secret);

		string HashString (string requestDescription, string secret);
	}
}