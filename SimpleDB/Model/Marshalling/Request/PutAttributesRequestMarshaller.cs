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
	}
}

