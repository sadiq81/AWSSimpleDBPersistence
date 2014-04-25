using System;
using System.Xml.Serialization;
using System.Net.Http;
using System.Net;
using System.Xml.Linq;

namespace AWSSimpleDBPersistence
{
	public class CreateDomainResponseUnMarshaller
	{
		protected XmlSerializer Serializer;

		public Response Response { get; set; }

		public CreateDomainResponseUnMarshaller (HttpResponseMessage message)
		{
			XDocument doc = XDocument.Load (message.Content.ReadAsStreamAsync ().Result);

			if (message.StatusCode.Equals (HttpStatusCode.OK)) {
				Serializer = new XmlSerializer (typeof(CreateDomainResponse), Response.NameSpace);
				Response = (CreateDomainResponse)Serializer.Deserialize (doc.CreateReader ());
				Response.HttpStatusCode = message.StatusCode;
				Response.ContentLength = (long)message.Content.Headers.ContentLength;
			} else {
				Serializer = new XmlSerializer (typeof(Response));
				Response = (Response)Serializer.Deserialize (doc.CreateReader ());
				Response.HttpStatusCode = message.StatusCode;
				Response.ContentLength = (long)message.Content.Headers.ContentLength;
			}
			Serializer = null;
		}
	}
}

