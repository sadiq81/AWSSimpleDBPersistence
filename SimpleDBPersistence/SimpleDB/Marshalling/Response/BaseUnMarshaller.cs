using System.Xml.Serialization;
using System.Net.Http;
using System.Xml.Linq;
using System.Net;
using SimpleDBPersistence.SimpleDB.Response;
using SimpleDBPersistence.SimpleDB.Model.AWSException;


namespace SimpleDBPersistence.SimpleDB.Marshalling.Response
{
	public class BaseUnMarshaller<T> : IUnMarshaller<T> where T: BaseResponse
	{
		private const string NameSpace = "http://sdb.amazonaws.com/doc/2009-04-15/";

		protected XmlSerializer Serializer { get; set; }

		protected HttpResponseMessage Message { get; set; }

		T Response { get; set; }

		public void Configure (HttpResponseMessage message)
		{
			this.Message = message;
		}

		public  T UnMarshal ()
		{
			XDocument doc = XDocument.Load (Message.Content.ReadAsStreamAsync ().Result);

			if (Message.StatusCode.Equals (HttpStatusCode.OK)) {
				Serializer = new XmlSerializer (typeof(T), NameSpace);
				Response = (T)Serializer.Deserialize (doc.CreateReader ());
				Response.HttpStatusCode = Message.StatusCode;
				Response.ContentLength = Message.Content.Headers.ContentLength;
				return Response;
			} else {
				Serializer = new XmlSerializer (typeof(BaseResponse));
				BaseResponse response = (BaseResponse)Serializer.Deserialize (doc.CreateReader ());
				response.HttpStatusCode = Message.StatusCode;
				response.ContentLength = Message.Content.Headers.ContentLength;
				throw new AWSErrorException (response);
			}
		}
	}
}

