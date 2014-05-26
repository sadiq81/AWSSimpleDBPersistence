using System;
using AWSSimpleDBPersistence;
using System.Collections.Generic;

namespace Test
{
	public class TestEntity : Entity
	{

		private string TestString { get; set;}

		private bool TestBool { get; set;}

		private byte TestByte { get; set;}

		private sbyte TestNegativeByte { get; set;}

		private Decimal TestDecimal { get; set;}

		private Decimal TestNegativeDecimal {get;set;}

		private List<string> TestList { get; set;}

		public TestEntity ()
		{
		}
	}
}

