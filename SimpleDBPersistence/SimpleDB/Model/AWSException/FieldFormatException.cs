using System;

namespace SimpleDBPersistence.SimpleDB.Model.AWSException
{
	public class FieldFormatException : AWSErrorException
	{
		private string FormatError;

		public FieldFormatException (string formatError)
		{
			this.FormatError = formatError;
		}

		public override string ToString ()
		{
			return string.Format ("[FieldFormatException] : {0} ", FormatError);
		}
	}
}

