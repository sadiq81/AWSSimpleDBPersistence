using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Runtime.CompilerServices;

namespace AWSSimpleDBPersistence
{
	public class BatchPutAttributesRequestMarshaller : BaseMarshaller
	{
		public  void Configure (BatchPutAttributesRequest request)
		{
			base.Configure (request);
			for (int itemCount = 0; itemCount < request.ReplaceableItems.Count; itemCount++) {

				ReplaceableItem item = request.ReplaceableItems [itemCount];
				Arguments.Add ("Item." + (itemCount) + ".ItemName", item.ItemName);

				for (int attributeCount = 0; attributeCount < item.ReplaceableAttributes.Count; attributeCount++) {
					ReplaceableAttribute attribute = item.ReplaceableAttributes [attributeCount];
					Arguments.Add ("Item." + (itemCount) + ".Attribute." + (attributeCount) + ".Name", attribute.Name);
					Arguments.Add ("Item." + (itemCount) + ".Attribute." + (attributeCount) + ".Value", attribute.Value);
					if (attribute.Replace) {
						Arguments.Add ("Item." + (itemCount) + ".Attribute." + (attributeCount) + ".Replace", "true");
					}
				}
			}
		}

		public override string Marshal ()
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

		public override string GenerateSignature ()
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
	}
}

