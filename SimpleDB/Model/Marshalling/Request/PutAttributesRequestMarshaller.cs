using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Runtime.CompilerServices;

namespace AWSSimpleDBPersistence
{
	public class PutAttributesRequestMarshaller : BaseMarshaller
	{
		public  void Configure (PutAttributesRequest request)
		{
			base.Configure (request);

			Arguments.Add ("ItemName", request.ItemName);

			for (int attributeCount = 0; attributeCount < request.ReplaceableAttributes.Count; attributeCount++) {
				ReplaceableAttribute attribute = request.ReplaceableAttributes [attributeCount];
				Arguments.Add ("Attribute." + (attributeCount) + ".Name", attribute.Name);
				Arguments.Add ("Attribute." + (attributeCount) + ".Value", attribute.Value);
				if (attribute.Replace) {
					Arguments.Add ("Attribute." + (attributeCount) + ".Replace.", "true");
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

