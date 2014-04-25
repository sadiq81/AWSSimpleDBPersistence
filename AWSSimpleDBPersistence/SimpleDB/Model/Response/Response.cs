using System;
using System.Net;
using System.Net.Http;
using System.Xml.Linq;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace AWSSimpleDBPersistence
{
	public class Response
	{
		public static string NameSpace = "http://sdb.amazonaws.com/doc/2009-04-15/";

		public ResponseMetadata ResponseMetadata{ get; set; }

		public long ContentLength{ get; set; }

		public HttpStatusCode HttpStatusCode { get; set; }

		public List<Error> Errors { get; set; }

		public string RequestID { get; set; }

		public Response ()
		{

		}

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

