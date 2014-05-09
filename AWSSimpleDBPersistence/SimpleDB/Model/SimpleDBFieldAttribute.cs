using System;

namespace AWSSimpleDBPersistence
{
	public class SimpleDBFieldAttribute : System.Attribute
	{
		public string Name { get; set; }

		public int ZeroPadding{ get; set; }

		public int Offset{ get; set; }

		public SimpleDBFieldAttribute (string name)
		{
			this.Name = name;
			this.ZeroPadding = 0;
			this.Offset = 0;
		}

		public SimpleDBFieldAttribute (string name, int zeroPadding)
		{
			this.Name = name;
			this.ZeroPadding = zeroPadding;
			this.Offset = 0;
		}

		public SimpleDBFieldAttribute (string name, int zeroPadding, int offset)
		{
			this.Name = name;
			this.ZeroPadding = zeroPadding;
			this.Offset = offset;
		}
	}
}

