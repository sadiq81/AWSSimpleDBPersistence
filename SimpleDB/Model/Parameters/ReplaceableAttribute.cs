using System;

namespace AWSSimpleDBPersistence
{
	public class ReplaceableAttribute : Attribute
	{
		public bool Replace { get; set; }

		public ReplaceableAttribute (string name, string value, bool replace) : base (name, value)
		{
			this.Replace = replace;
		}
	}
}

