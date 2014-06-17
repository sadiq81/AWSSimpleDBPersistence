
using SimpleDBPersistence.SimpleDB.Request;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Request
{
	public class GetAttributesRequestMarshaller : BaseMarshaller
	{
		public  void Configure (GetAttributesRequest request)
		{
			base.Configure (request);

			Arguments.Add ("ItemName", request.ItemName);

			if (request.AttributeNames != null) {
				for (int attributeCount = 0; attributeCount < request.AttributeNames.Count; attributeCount++) {
					string attribute = request.AttributeNames [attributeCount];
					Arguments.Add ("AttributeName." + (attributeCount), attribute);

				}
			}

			Arguments.Add ("ConsistentRead", request.ConsistentRead.ToString ().ToLower ());
		}
	}
}

