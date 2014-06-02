using System;
using SimpleDBPersistence.SimpleDB.Request;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Request
{
	public class SelectRequestMarshaller : BaseMarshaller
	{
		public  void Configure (SelectRequest request)
		{
			Arguments.Add ("SignatureMethod", SignatureMethod);
			Arguments.Add ("SignatureVersion", SignatureVersion);
			Arguments.Add ("Timestamp", DateTime.UtcNow.ToString ("o"));
			Arguments.Add ("Version", Version);

			Arguments.Add ("SelectExpression", request.SelectExpression);

			Action = "Select";

			if (request != null && request.NextToken != null) {
				Arguments.Add ("NextToken", request.NextToken);
			}

			if (request != null && request.ConsistentRead) {
				Arguments.Add ("ConsistentRead", "true");
			}
		}
	}
}

