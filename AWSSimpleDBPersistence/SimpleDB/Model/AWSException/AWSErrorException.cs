using System;

namespace AWSSimpleDBPersistence
{
	public class AWSErrorException : Exception
	{
		public Response Response { get; set; }

		public AWSErrorException (Response response)
		{
			this.Response = response;
		}

		public override string ToString ()
		{
			return string.Format ("[AWSErrorException: RequestID={0}, HttpStatusCode={1}, Errors={2}]", Response.RequestID, Response.HttpStatusCode, Response.Errors);
		}
	}
}

