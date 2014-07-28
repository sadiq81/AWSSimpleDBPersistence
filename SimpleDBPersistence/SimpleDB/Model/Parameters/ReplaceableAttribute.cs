using System;

namespace SimpleDBPersistence.SimpleDB.Model.Parameters
{
	public class ReplaceableAttribute : Attribute
	{
		public bool Replace { get; set; }

		public ReplaceableAttribute (string name, string value, bool replace) : base (name, value)
		{
			this.Replace = replace;
		}

		public override string ToString ()
		{
			return string.Format ("[ReplaceableAttribute: Name={0}, Value={1} ,Replace={2}]", Name, Value, Replace);
		}
		
	}
}

