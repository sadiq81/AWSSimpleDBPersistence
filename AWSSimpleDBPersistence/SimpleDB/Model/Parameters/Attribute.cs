using System;

namespace AWSSimpleDBPersistence
{
	public class Attribute
	{
		public Attribute ()
		{
		}

		public Attribute (string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		public string Name { get; set; }

		public string Value { get; set; }
	}
}

