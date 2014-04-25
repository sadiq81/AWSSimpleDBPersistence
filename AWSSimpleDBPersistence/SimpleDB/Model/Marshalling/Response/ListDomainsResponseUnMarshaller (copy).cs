using System;
using System.Xml.Serialization;
using System.Net.Http;
using System.Net;
using System.Xml.Linq;

namespace AWSSimpleDBPersistence
{
	public class ListDomainsResponseUnMarshaller
	{
		protected XmlSerializer Serializer;

		public Response Response { get; set; }

		public ListDomainsResponseUnMarshaller (HttpResponseMessage message)
		{
			XDocument doc = XDocument.Load (message.Content.ReadAsStreamAsync ().Result);

			if (message.StatusCode.Equals (HttpStatusCode.OK)) {
				Serializer = new XmlSerializer (typeof(ListDomainsResponse), Response.NameSpace);
				Response = (ListDomainsResponse)Serializer.Deserialize (doc.CreateReader ());
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

