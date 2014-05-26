using System.Net;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class Response
	{
		public ResponseMetadata ResponseMetadata{ get; set; }

		public long? ContentLength{ get; set; }

		public HttpStatusCode HttpStatusCode { get; set; }

		public List<Error> Errors { get; set; }

		public class Error
		{
			public string Code { get; set; }

			public string Message { get; set; }

			public override string ToString ()
			{
				return string.Format ("[Error: Code={0}, Message={1}]", Code, Message);
			}
		}
	}
}

