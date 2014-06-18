SimpleDBPersistence
======================

Setup:

Before programstart the ServiceContainer must be initialized with an AmazonSimpleDBClient and you need to implement a SHA256 HmacService

For example like this:

    ServiceContainer.Register<AmazonSimpleDBClient> (() => new AmazonSimpleDBClient ("AWSAccessKey", "AWSSecretKey", RegionEndpoint.EUWest1));
    ServiceContainer.Register<ISHA256Service> (() => new SHA256Service ());

Look in Sample for SHA256Service example

Then add Attributes to Entities like so

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
    
Create DAO Objects for entities

	using SimpleDBPersistence.DAO;

	namespace SimpleDBTest
	{
		public class TestEntityDAO : GenericDAO<TestEntity>
		{
		}
	}  
    
Save Entities like so:

	bool result = DAO.CreateTable ().Result;
	Assert.AreEqual (true, result);

	TestEntity entity = new TestEntity ();
	entity.Id = 0;
	entity.TestString = "TestString";
	entity.TestBool = false;
	entity.TestByte = 244;
	entity.TestNegativeByte = -50;
	entity.TestDecimal = 500;
	entity.TestNegativeDecimal = -500;
	entity.TestList = new List<string> (new[]{ "hello", "dolly", "the", "sheep" });   
	DAO.SaveOrReplace (entity).Result;
	
See more examples and full source code at https://github.com/sadiq81/AWSSimpleDBPersistence