using System;

namespace AWSSimpleDBPersistence
{
	public class SelectRequest
	{
		public string SelectExpression { get; set; }

		public string NextToken { get; set; }

		public bool ConsistentRead  { get; set; }
	}
}

