using System;
using SimpleDBPersistence.SimpleDB.Response;
using System.Text;

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
			StringBuilder sb = new StringBuilder ();
			foreach (BaseResponse.Error error in Response.Errors) {
				sb.Append (error.ToString () + " ");
			}
			return string.Format ("[AWSErrorException: HttpStatusCode={0}, Errors={1}]", Response.HttpStatusCode, sb.ToString ());
		}
	}
}

