using System;

namespace SimpleDBPersistence.SimpleDB.Model.Parameters
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

		public override string ToString ()
		{
			return string.Format ("[Attribute: Name={0}, Value={1}]", Name, Value);
		}
		
	}
}

