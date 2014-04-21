using System;

namespace AWSSimpleDBPersistence
{
	public class SimpleDBFieldAttribute : System.Attribute
	{
		public string Name { get; set; }

		public SimpleDBFieldAttribute (string name)
		{
			this.Name = name;
		}
	}
}

