using System;
using System.Net;

namespace AWSSimpleDBPersistence
{
	public class Response
	{
		public ResponseMetadata ResponseMetadataField{ get; set; }

		public long ContentLength{ get; set; }

		public HttpStatusCode HttpStatusCode { get; set; }

		public Response ()
		{
		}
	}
}

