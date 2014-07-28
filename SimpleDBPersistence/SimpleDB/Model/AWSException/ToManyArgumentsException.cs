using System;

namespace SimpleDBPersistence.SimpleDB.Model.AWSException
{
	public class ToManyArgumentsException : AWSErrorException
	{

		public override string ToString ()
		{
			return string.Format ("[ToManyArgumentsException: Only 5 comparisons within a single predicate allowed ]");
		}

	}
}

