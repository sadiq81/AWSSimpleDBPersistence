using System;
using System.Collections.Generic;
using System.Text;
using SimpleDBPersistence.SimpleDB.Request;
using SimpleDBPersistence.Service;
using SimpleDBPersistence.SimpleDB.Utils;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Request
{
	public  abstract class BaseMarshaller : IMarshaller
	{
		protected const string Protocol = "https";
		protected const string Method = "POST";
		protected const string SignatureMethod = "HmacSHA256";
		protected const string SignatureVersion = "2";
		protected const string Version = "2009-04-15";
		protected string Action;
		protected  SortedDictionary<string, string> Arguments = new SortedDictionary<string, string> ();

		public void Configure (DomainRequest request)
		{
			Arguments.Add ("DomainName", request.DomainName);
			Arguments.Add ("SignatureMethod", SignatureMethod);
			Arguments.Add ("SignatureVersion", SignatureVersion);
			Arguments.Add ("Timestamp", DateTime.UtcNow.ToString ("o"));
			Arguments.Add ("Version", Version);
			this.Action = request.GetType ().Name.Replace ("Request", "");
		}

		public IEnumerable<KeyValuePair<string, string>> MarshallPost ()
		{
			SortedDictionary<string, string> content = new SortedDictionary<string, string> (Arguments);
			content.Add ("Action", Action);
			content.Add ("AWSAccessKeyId", AWSAccessKeyId);
			content.Add ("Signature", GenerateSignature ());
			return content;
		}

		public  string GenerateSignature ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (string.Format ("{0}{1}{2}{3}/{4}", Method, Environment.NewLine, Region, Environment.NewLine, Environment.NewLine));
			sb.Append (string.Format ("AWSAccessKeyId={0}", AWSAccessKeyId));
			sb.Append (string.Format ("&Action={0}", Action));

			if (Items != null) {
				sb.Append (Items);
			}

			string signature = sb.ToString ();
			ISHA256Service service = ServiceContainer.Resolve<ISHA256Service> ();
			string hashed = service.CreateHash (signature, AWSSecretKey);
			return hashed;
		}

		protected  string AWSAccessKeyId {
			get {
				return Client.AWSAccessKeyId;
			}
		}

		protected  string AWSSecretKey {
			get {
				return Client.AWSSecretKey;
			}
		}

		protected  string Region {
			get {
				return Client.Region;
			}
		}

		protected  string Items {
			get {
				StringBuilder sb = new StringBuilder ();
				var enumerator = Arguments.GetEnumerator ();
				while (enumerator.MoveNext ()) {
					var entry = enumerator.Current;
					sb.Append ("&");
					sb.Append (entry.Key.ToRfc3986 ());
					sb.Append ("=");
					sb.Append (entry.Value.ToRfc3986 ());
				}
				return sb.ToString ();
			}
		}

		protected  SimpleDBClientCore Client {
			get {
				return ServiceContainer.Resolve<SimpleDBClientCore> ();
			}
		}
	}
}

