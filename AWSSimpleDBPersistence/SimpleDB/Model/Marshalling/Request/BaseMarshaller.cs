using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace AWSSimpleDBPersistence
{
	public  abstract class BaseMarshaller : IMarshaller
	{
		protected const string Protocol = "https";
		protected const string Method = "GET";
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

		public  string Marshal ()
		{
			StringBuilder sb = new StringBuilder ();

			sb.Append (string.Format ("{0}://{1}/", Protocol, Region));
			sb.Append (string.Format ("?Action={0}", Action));
			sb.Append (string.Format ("&AWSAccessKeyId={0}", AWSAccessKeyId));

			sb.Append (Items);

			string signature = GenerateSignature ();
			string encoded = WebUtility.UrlEncode (signature);

			sb.Append (string.Format ("&Signature={0}", encoded));

			return sb.ToString ();
		}

		public  string GenerateSignature ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (string.Format ("{0}{1}{2}{3}/{4}", Method, Environment.NewLine, Region, Environment.NewLine, Environment.NewLine));
			sb.Append (string.Format ("AWSAccessKeyId={0}", AWSAccessKeyId));
			sb.Append (string.Format ("&Action={0}", Action));
			sb.Append (Items);

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
					sb.Append (WebUtility.UrlEncode (entry.Key));
					sb.Append ("=");
					sb.Append (WebUtility.UrlEncode (entry.Value));
				}
				return sb.ToString ();
			}
		}

		protected  SimpleDBClient Client {
			get {
				return ServiceContainer.Resolve<SimpleDBClient> ();
			}
		}
	}
}

