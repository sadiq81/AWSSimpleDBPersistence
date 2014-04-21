using System;
using System.Text;
using System.Collections.Generic;
using System.Dynamic;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Net;

namespace AWSSimpleDBPersistence
{
	public class BatchPutAttributesRequestMarshaller
	{
		private const string PROTOCOL = "https";
		private const string ACTION = "BatchPutAttributes";
		private const string METHOD = "GET";
		private const string SIGNATURE_METHOD = "HmacSHA256";
		private const string SIGNATURE_VERSION = "2";
		private const string VERSION = "2009-04-15";
		private string TimeStamp;
		private SortedDictionary<string, string> Arguments = new SortedDictionary<string, string> ();
		private BatchPutAttributesRequest request;

		public BatchPutAttributesRequestMarshaller (BatchPutAttributesRequest request)
		{
			this.request = request;

			for (int itemCount = 0; itemCount < request.Items.Count; itemCount++) {

				Item item = request.Items [itemCount];
				Arguments.Add ("Item." + (itemCount) + ".ItemName", item.ItemName);

				for (int attributeCount = 0; attributeCount < item.Attributes.Count; attributeCount++) {
					ReplaceableAttribute attribute = item.Attributes [attributeCount];
					Arguments.Add ("Item." + (itemCount) + ".Attribute." + (attributeCount) + ".Name", attribute.Name);
					Arguments.Add ("Item." + (itemCount) + ".Attribute." + (attributeCount) + ".Value", attribute.Value);
					if (attribute.Replace) {
						Arguments.Add ("Item." + (itemCount) + ".Attribute." + (attributeCount) + ".Replace", "true");
					}
				}
			}
			//TimeStamp = "2014-04-20T22:15:00.4558970Z";
			TimeStamp = DateTime.UtcNow.ToString ("o");
		}

		public String Marshal ()
		{
			StringBuilder sb = new StringBuilder ();

			sb.Append ("https://" + Region + "/");
			sb.Append ("?Action=" + ACTION);
			sb.Append ("&DomainName=" + request.DomainName);
			sb.Append (Items);
			sb.Append ("&Version=" + VERSION);
			sb.Append ("&Timestamp=" + WebUtility.UrlEncode (TimeStamp));

			string signature = GenerateSignature ();
			string encoded = WebUtility.UrlEncode (signature);

			sb.Append ("&Signature=" + encoded);
			sb.Append ("&SignatureVersion=" + SIGNATURE_VERSION);
			sb.Append ("&SignatureMethod=" + SIGNATURE_METHOD);
			sb.Append ("&AWSAccessKeyId=" + AWSAccessKeyId);

			return sb.ToString ();
		}

		private string GenerateSignature ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (METHOD + Environment.NewLine);
			sb.Append (Region + Environment.NewLine);
			sb.Append ("/" + Environment.NewLine);
			sb.Append ("AWSAccessKeyId=" + AWSAccessKeyId);
			sb.Append ("&Action=" + ACTION);
			sb.Append ("&DomainName=" + request.DomainName);
			sb.Append (Items);
			sb.Append ("&SignatureMethod=" + SIGNATURE_METHOD);
			sb.Append ("&SignatureVersion=" + SIGNATURE_VERSION);
			sb.Append ("&Timestamp=" + WebUtility.UrlEncode (TimeStamp));
			sb.Append ("&Version=" + VERSION);

			string signature = sb.ToString ();

			ISHA256Service service = ServiceContainer.Resolve<ISHA256Service> ();

			string hashed = service.CreateHash (signature, AWSSecretKey);

			return hashed;
		}

		private string Items {
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

		private  string AWSAccessKeyId {
			get {
				return ServiceContainer.Resolve<SimpleDBClient> ().AWSAccessKeyId;
			}
		}

		private  string AWSSecretKey {
			get {
				return ServiceContainer.Resolve<SimpleDBClient> ().AWSSecretKey;
			}
		}

		public  string Region {
			get {
				return ServiceContainer.Resolve<SimpleDBClient> ().Region;
			}
		}

		private  SimpleDBClient Client {
			get {
				return ServiceContainer.Resolve<SimpleDBClient> ();
			}
		}
	}
}

