
using SimpleDBPersistence.SimpleDB.Request;
using SimpleDBPersistence.SimpleDB.Model.Parameters;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Request
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

