using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Runtime.CompilerServices;

namespace AWSSimpleDBPersistence
{
	public class BatchDeleteAttributesRequestMarshaller : BaseMarshaller
	{
		public  void Configure (BatchDeleteAttributesRequest request)
		{
			base.Configure (request);
			for (int itemCount = 0; itemCount < request.Items.Count; itemCount++) {

				Item item = request.Items [itemCount];
				Arguments.Add ("Item." + (itemCount) + ".ItemName", item.Name);

				//TODO should this be implemtented?
				/*for (int attributeCount = 0; attributeCount < item.Attributes.Count; attributeCount++) {
					Attribute attribute = item.Attributes [attributeCount];
					Arguments.Add ("Item." + (itemCount) + ".Attribute." + (attributeCount) + ".Name", attribute.Name);
					Arguments.Add ("Item." + (itemCount) + ".Attribute." + (attributeCount) + ".Value", attribute.Value);
				}*/
			}
		}
	}
}

