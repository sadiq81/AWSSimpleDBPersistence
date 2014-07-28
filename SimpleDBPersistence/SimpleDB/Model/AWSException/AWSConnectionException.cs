using System;
using System.Net;

namespace SimpleDBPersistence.SimpleDB.Model.AWSException
{
	public class AWSConnectionException : AWSErrorException
	{
		private readonly Exception Exception;

		public AWSConnectionException (WebException exception)
		{
			Exception = exception;
		}

		public override string ToString ()
		{
			return string.Format ("[AWSConnectionException: Exception={0}]", Exception);
		}
		
	}
}

