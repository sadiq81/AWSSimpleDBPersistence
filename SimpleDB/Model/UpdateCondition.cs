using System;

namespace AWSSimpleDBPersistence
{
	public class UpdateCondition
	{
		public string Name { get; set; }

		public string Value { get; set; }

		public bool Exists { get; set; }

		public UpdateCondition ()
		{
		}
	}
}

