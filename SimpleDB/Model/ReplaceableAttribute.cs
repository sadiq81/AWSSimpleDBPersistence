using System;

namespace AWSSimpleDBPersistence
{
	public class ReplaceableAttribute
	{
		public string Name { get; set; }

		public string Value { get; set; }

		public bool Replace { get; set; }
	}
}

