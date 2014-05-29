using System;
using AWSSimpleDBPersistence;
using System.Collections.Generic;

namespace Test
{
	[SimpleDBDomain ("TestDao")]
	public class TestEntity : Entity
	{
		[SimpleDBField ("TestString")]
		public string TestString { get; set; }

		[SimpleDBField ("TestBool")]
		public bool TestBool { get; set; }

		[SimpleDBField ("TestByte", 3)]
		public byte TestByte { get; set; }

		[SimpleDBField ("TestNegativeByte", 3, 255)]
		public sbyte TestNegativeByte { get; set; }

		[SimpleDBField ("TestDecimal", 5, 1000)]
		public Decimal TestDecimal { get; set; }

		[SimpleDBField ("TestNegativeDecimal", 5, 1000)]
		public Decimal TestNegativeDecimal { get; set; }

		[SimpleDBField ("TestList")]
		public List<string> TestList { get; set; }

	}
}

