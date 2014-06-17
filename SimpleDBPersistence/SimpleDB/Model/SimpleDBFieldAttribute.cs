using System;
using SimpleDBPersistence.SimpleDB.Model.AWSException;

namespace SimpleDBPersistence.SimpleDB.Model
{
	public class SimpleDBFieldAttribute : System.Attribute
	{
		public string Name { get; set; }

		public byte ZeroPadding{ get; set; }

		public double Offset{ get; set; }

		public SimpleDBFieldAttribute (string name)
		{
			this.Name = name;
			this.ZeroPadding = 0;
			this.Offset = 0;
		}

		public SimpleDBFieldAttribute (string name, byte zeroPadding)
		{
			this.Name = name;
			this.ZeroPadding = zeroPadding;
			this.Offset = 0;
		}

		public SimpleDBFieldAttribute (string name, byte zeroPadding, double offset)
		{
			this.Name = name;
			this.ZeroPadding = zeroPadding;
			this.Offset = offset;
		}

		public static string ApplyOffset (SimpleDBFieldAttribute attribute, decimal value)
		{

			decimal offset = (decimal)attribute.Offset; 
			if (offset > 0) {
				value = value + offset;
				if (value < 0) {
					throw new  FieldFormatException ("Negative value of attribute " + attribute.Name + " is greather than specified offset");
				} 
			}
			return value.ToString ();
		}


		public static string ApplyPadding (SimpleDBFieldAttribute attribute, string value)
		{
			int padding = attribute.ZeroPadding;
			int length = value.Length;
			if (padding > 0) {
				if (length > padding) {
					throw new  FieldFormatException ("String length of value is greather than padding specified in attribute " + attribute.Name);
				} else {
					return value.PadLeft (padding, '0');
				}
			}
			return value;
		}
	}
}

