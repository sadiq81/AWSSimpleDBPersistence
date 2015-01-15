using System;
using System.Globalization;

namespace SimpleDBPersistence.SimpleDB.Utils
{
	public class Constants
	{
		public static CultureInfo CultureInfo {
			get { 
				CultureInfo ci = CultureInfo.InvariantCulture;
				ci.NumberFormat.NumberDecimalSeparator = ".";
				return ci;
			}
			private set{ }
		}
	}
}

