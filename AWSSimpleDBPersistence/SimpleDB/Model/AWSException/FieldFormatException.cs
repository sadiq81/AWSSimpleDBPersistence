using System;

namespace AWSSimpleDBPersistence
{
	public class FieldFormatException : Exception
	{
		private string FormatError;

		public FieldFormatException (string formatError)
		{
			this.FormatError = formatError;
		}

		public override string Message {
			get {
				return FormatError;
			}
		}
	}
}

