using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Runtime.CompilerServices;

namespace AWSSimpleDBPersistence
{
	public class DeleteAttributesRequestMarshaller : BaseMarshaller
	{
		public  void Configure (DeleteAttributesRequest request)
		{
			base.Configure (request);

			Arguments.Add ("ItemName", request.ItemName);

			if (request.Attributes != null) {
				for (int attributeCount = 0; attributeCount < request.Attributes.Count; attributeCount++) {
					Attribute attribute = request.Attributes [attributeCount];
					Arguments.Add ("Attribute." + (attributeCount) + ".Name", attribute.Name);
					Arguments.Add ("Attribute." + (attributeCount) + ".Value", attribute.Value);

				}
			}
			if (request.Expected != null) {
				Arguments.Add ("Expected.Name", request.Expected.Name);
				Arguments.Add ("Expected.Value", request.Expected.Value);
			}
		}
	}
}

