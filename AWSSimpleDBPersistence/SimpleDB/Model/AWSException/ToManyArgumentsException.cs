using System;

namespace AWSSimpleDBPersistence
{
	public class ToManyArgumentsException : Exception
	{
		public override string Message {
			get {
				return "Only 5 comparisons within a single predicate allowed";
			}
		}
	}
}

