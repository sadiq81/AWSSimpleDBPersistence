
using SimpleDBPersistence.SimpleDB.Request;
using SimpleDBPersistence.SimpleDB.Model.Parameters;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Request
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

