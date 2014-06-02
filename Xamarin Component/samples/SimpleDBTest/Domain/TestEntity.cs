using System.Collections.Generic;
using SimpleDBPersistence.SimpleDB.Model;
using SimpleDBPersistence.Domain;

namespace SimpleDBTest
{
	[SimpleDBDomain ("TestDao")]
	public class TestEntity : Entity
	{
		[SimpleDBField ("TestString")]
		public string TestString { get; set; }

		[SimpleDBField ("TestBool")]
		public bool? TestBool { get; set; }

		[SimpleDBField ("TestByte", 3)]
		public byte? TestByte { get; set; }

		[SimpleDBField ("TestNegativeByte", 3, 255)]
		public sbyte? TestNegativeByte { get; set; }

		[SimpleDBField ("TestDecimal", 5, 1000)]
		public decimal? TestDecimal { get; set; }

		[SimpleDBField ("TestNegativeDecimal", 5, 1000)]
		public decimal? TestNegativeDecimal { get; set; }

		[SimpleDBField ("TestList")]
		public List<string> TestList { get; set; }

	}
}

