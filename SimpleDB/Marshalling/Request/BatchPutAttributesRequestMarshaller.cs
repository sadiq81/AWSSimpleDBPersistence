
using SimpleDBPersistence.SimpleDB.Request;
using SimpleDBPersistence.SimpleDB.Model.Parameters;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Request
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
	}
}

