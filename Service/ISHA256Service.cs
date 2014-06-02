using System;

namespace SimpleDBPersistence.Service
{
	public interface ISHA256Service
	{
		string CreateHash (string message, string secret);

	}
}