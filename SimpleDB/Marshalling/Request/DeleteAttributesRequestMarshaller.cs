using Attribute = SimpleDBPersistence.SimpleDB.Model.Parameters.Attribute;
using SimpleDBPersistence.SimpleDB.Request;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Request
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

