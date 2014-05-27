using System;

namespace AWSSimpleDBPersistence
{
	public class SelectRequest
	{
		public string SelectExpression { get; set; }

		public string NextToken { get; set; }

		public bool ConsistentRead  { get; set; }

		public SelectRequest ()
		{
		}
		

		public SelectRequest (string selectExpression)
		{
			this.SelectExpression = selectExpression;
		}

		public SelectRequest (string selectExpression, string nextToken)
		{
			this.SelectExpression = selectExpression;
			this.NextToken = nextToken;
		}

		public SelectRequest (string selectExpression, bool consistentRead)
		{
			this.SelectExpression = selectExpression;
			this.ConsistentRead = consistentRead;
		}
		

		public SelectRequest (string selectExpression, string nextToken, bool consistentRead)
		{
			this.SelectExpression = selectExpression;
			this.NextToken = nextToken;
			this.ConsistentRead = consistentRead;
		}
		
	}
}

