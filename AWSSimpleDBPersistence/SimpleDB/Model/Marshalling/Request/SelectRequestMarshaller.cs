using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Runtime.CompilerServices;

namespace AWSSimpleDBPersistence
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

