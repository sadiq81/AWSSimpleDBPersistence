using System;

namespace AWSSimpleDBPersistence
{
	public class Expected
	{
		public string Name { get; set; }

		public string Value { get; set; }

		public bool Exists { get; set; }

		public Expected (string name)
		{
			this.Name = name;
		}

		public Expected (string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		public Expected (string name, string value, bool exists)
		{
			this.Name = name;
			this.Value = value;
			this.Exists = exists;
		}
	}
}

