using System;
using SimpleDBPersistence.SimpleDB.Response;

namespace SimpleDBPersistence.SimpleDB.Model.AWSException
{
	public class AWSErrorException : Exception
	{
		public BaseResponse Response { get; set; }

		public AWSErrorException (BaseResponse response)
		{
			this.Response = response;
		}

		public override string ToString ()
		{
			return string.Format ("[AWSErrorException: RequestID={0}, HttpStatusCode={1}, Errors={2}]", Response.ResponseMetadata.RequestId, Response.HttpStatusCode, Response.Errors);
		}
	}
}

