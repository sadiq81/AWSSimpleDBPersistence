using NUnit.Framework;
using System;
using System.Net;
using System.Collections.Generic;
using Attribute = SimpleDBPersistence.SimpleDB.Model.Parameters.Attribute;
using SimpleDBPersistence.SimpleDB;
using SimpleDBPersistence.Service;
using SimpleDBPersistence.SimpleDB.Response;
using SimpleDBPersistence.SimpleDB.Request;
using SimpleDBPersistence.SimpleDB.Model.Parameters;

namespace SimpleDBTest
{
	[TestFixture ()]
	public class TestDAO
	{

		TestEntityDAO DAO;
		SimpleDBClientCore Client;

		[TestFixtureSetUp ()]
		public void BeforeAllTests ()
		{
			ServiceContainer.Register<ISHA256Service> (() => new SHA256Service ());
			ServiceContainer.Register<SimpleDBClientCore> (() => new SimpleDBClientCore (Properties.AWSAccessKey, Properties.AWSSecretKey, Region.EUWest_1));
			ServiceContainer.Register<TestEntityDAO> (() => new TestEntityDAO ());
			Client = ServiceContainer.Resolve<SimpleDBClientCore> ();
			DAO = ServiceContainer.Resolve<TestEntityDAO> ();
		}

		[SetUp ()]
		public  void BeforeEachTest ()
		{

		}

		[TearDown ()]
		public  void AfterEachTest ()
		{
			ListDomainsResponse response = Client.ListDomains (new ListDomainsRequest ()).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);

			if (response.ListDomainsResult.DomainName != null) {
				foreach (string domainName in response.ListDomainsResult.DomainName) {
					DeleteDomainResponse response2 = Client.DeleteDomain (new DeleteDomainRequest (domainName)).Result;
					Assert.AreEqual (HttpStatusCode.OK, response2.HttpStatusCode);
				}
			}
		}

		[Test ()]
		public void TestCreateTable ()
		{
			bool result = DAO.CreateTable ().Result;
			Assert.AreEqual (true, result);
			ListDomainsResponse response = Client.ListDomains (new ListDomainsRequest ()).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
			Assert.AreEqual (1, response.ListDomainsResult.DomainName.Length);
			Assert.AreEqual ("TestDao", response.ListDomainsResult.DomainName [0]);
		}

		[Test ()]
		public void TestDeleteTable ()
		{
			bool result = DAO.CreateTable ().Result;
			Assert.AreEqual (true, result);
			ListDomainsResponse response = Client.ListDomains (new ListDomainsRequest ()).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
			Assert.AreEqual (1, response.ListDomainsResult.DomainName.Length);
			Assert.AreEqual ("TestDao", response.ListDomainsResult.DomainName [0]);

			bool result2 = DAO.DeleteTable ().Result;
			Assert.AreEqual (true, result2);
			ListDomainsResponse response2 = Client.ListDomains (new ListDomainsRequest ()).Result;
			Assert.AreEqual (HttpStatusCode.OK, response2.HttpStatusCode);
			Assert.AreEqual (null, response2.ListDomainsResult.DomainName);

		}

		[Test ()]
		public void TestSaveOrReplace ()
		{
			//[SimpleDBDomain("TestDao")]
			bool result = DAO.CreateTable ().Result;
			Assert.AreEqual (true, result);

			TestEntity entity = new TestEntity ();
			entity.Id = 0;

			//[SimpleDBField("TestString")]
			entity.TestString = "TestString";
			//[SimpleDBField("TestBool")]
			entity.TestBool = false;
			//[SimpleDBField("TestByte",3)]
			entity.TestByte = 244;
			//[SimpleDBField("TestNegativeByte",3, 255)]
			entity.TestNegativeByte = -50;
			//[SimpleDBField("TestDecimal",5,1000)]
			entity.TestDecimal = 500;
			//[SimpleDBField("TestNegativeDecimal",5,1000)]
			entity.TestNegativeDecimal = -500;
			//[SimpleDBField("TestList")]
			entity.TestList = new List<string> (new[]{ "hello", "dolly", "the", "sheep" });   
			bool result2 = DAO.SaveOrReplace (entity).Result;
			Assert.AreEqual (true, result2);

			GetAttributesRequest request = new GetAttributesRequest ("TestDao", "0", true);
			GetAttributesResponse response = Client.GetAttributes (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);

			Attribute TestString = Array.Find (response.GetAttributesResult, s => s.Name.Equals ("TestString"));
			Assert.IsNotNull (TestString);
			Assert.AreEqual ("TestString", TestString.Value);

			Attribute TestBool = Array.Find (response.GetAttributesResult, s => s.Name.Equals ("TestBool"));
			Assert.IsNotNull (TestBool);
			Assert.AreEqual ("False", TestBool.Value);

			Attribute TestByte = Array.Find (response.GetAttributesResult, s => s.Name.Equals ("TestByte"));
			Assert.IsNotNull (TestByte);
			Assert.AreEqual ("244", TestByte.Value);

			Attribute TestNegativeByte = Array.Find (response.GetAttributesResult, s => s.Name.Equals ("TestNegativeByte"));
			Assert.IsNotNull (TestNegativeByte);
			Assert.AreEqual ("205", TestNegativeByte.Value);

			Attribute TestDecimal = Array.Find (response.GetAttributesResult, s => s.Name.Equals ("TestDecimal"));
			Assert.IsNotNull (TestDecimal);
			Assert.AreEqual ("01500", TestDecimal.Value);

			Attribute TestNegativeDecimal = Array.Find (response.GetAttributesResult, s => s.Name.Equals ("TestNegativeDecimal"));
			Assert.IsNotNull (TestNegativeDecimal);
			Assert.AreEqual ("00500", TestNegativeDecimal.Value);

			Attribute TestList = Array.Find (response.GetAttributesResult, s => s.Name.Equals ("TestList"));
			Assert.IsNotNull (TestList);
			Assert.AreEqual ("[\"hello\",\"dolly\",\"the\",\"sheep\"]", TestList.Value);

		}

		[Test ()]
		public void TestDelete ()
		{
			bool result = DAO.CreateTable ().Result;
			Assert.AreEqual (true, result);

			TestEntity entity = new TestEntity ();
			entity.Id = 0;

			bool result2 = DAO.SaveOrReplace (entity).Result;
			Assert.AreEqual (true, result2);

			//Wait for save to be propagated
			System.Threading.Thread.Sleep (3000);

			DomainMetadataRequest request = new DomainMetadataRequest ("TestDao");
			DomainMetadataResponse response = Client.DomainMetadata (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
			Assert.AreEqual ("1", response.DomainMetadataResult.ItemCount);

			bool result3 = DAO.Delete (entity).Result;
			Assert.AreEqual (true, result3);

			//Wait for save to be propagated
			System.Threading.Thread.Sleep (3000);

			DomainMetadataRequest request2 = new DomainMetadataRequest ("TestDao");
			DomainMetadataResponse response2 = Client.DomainMetadata (request2).Result;
			Assert.AreEqual (HttpStatusCode.OK, response2.HttpStatusCode);
			Assert.AreEqual ("0", response2.DomainMetadataResult.ItemCount);

		}

		[Test ()]
		public void TestDeleteMultiple ()
		{
			bool result = DAO.CreateTable ().Result;
			Assert.AreEqual (true, result);

			TestEntity entity = new TestEntity ();
			entity.Id = 0;

			bool result2 = DAO.SaveOrReplace (entity).Result;
			Assert.AreEqual (true, result2);

			TestEntity entity1 = new TestEntity ();
			entity1.Id = 1;

			bool result3 = DAO.SaveOrReplace (entity1).Result;
			Assert.AreEqual (true, result3);

			//Wait for save to be propagated
			System.Threading.Thread.Sleep (3000);

			DomainMetadataRequest request = new DomainMetadataRequest ("TestDao");
			DomainMetadataResponse response = Client.DomainMetadata (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
			Assert.AreEqual ("2", response.DomainMetadataResult.ItemCount);

			bool result4 = DAO.DeleteMultiple (new List<TestEntity> (){ entity, entity1 }).Result;
			Assert.AreEqual (true, result4);

			//Wait for save to be propagated
			System.Threading.Thread.Sleep (3000);

			DomainMetadataRequest request2 = new DomainMetadataRequest ("TestDao");
			DomainMetadataResponse response2 = Client.DomainMetadata (request2).Result;
			Assert.AreEqual (HttpStatusCode.OK, response2.HttpStatusCode);
			Assert.AreEqual ("0", response2.DomainMetadataResult.ItemCount);
		}

		[Test ()]
		public void TestSaveOrReplaceMultiple ()
		{
			bool result = DAO.CreateTable ().Result;
			Assert.AreEqual (true, result);

			TestEntity entity = new TestEntity ();
			entity.Id = 0;

			TestEntity entity1 = new TestEntity ();
			entity1.Id = 1;

			bool result3 = DAO.SaveOrReplaceMultiple (new List<TestEntity> (){ entity, entity1 }).Result;
			Assert.AreEqual (true, result3);

			//Wait for save to be propagated
			System.Threading.Thread.Sleep (3000);

			DomainMetadataRequest request = new DomainMetadataRequest ("TestDao");
			DomainMetadataResponse response = Client.DomainMetadata (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
			Assert.AreEqual ("2", response.DomainMetadataResult.ItemCount);
		}

		[Test ()]
		public void TestSelect ()
		{
			//[SimpleDBDomain("TestDao")]
			bool result = DAO.CreateTable ().Result;
			Assert.AreEqual (true, result);

			TestEntity entity = new TestEntity ();
			entity.Id = 0;

			//[SimpleDBField("TestString")]
			entity.TestString = "plz";
			//[SimpleDBField("TestBool")]
			entity.TestBool = false;
			//[SimpleDBField("TestByte",3)]
			entity.TestByte = 244;
			//[SimpleDBField("TestNegativeByte",3, 255)]
			entity.TestNegativeByte = -50;
			//[SimpleDBField("TestDecimal",5,1000)]
			entity.TestDecimal = 500;
			//[SimpleDBField("TestNegativeDecimal",5,1000)]
			entity.TestNegativeDecimal = -500;
			//[SimpleDBField("TestList")]
			entity.TestList = new List<string> (new[]{ "hello", "dolly", "the", "sheep" });   
			bool result2 = DAO.SaveOrReplace (entity).Result;
			Assert.AreEqual (true, result2);

			//Equal
			SelectQuery<TestEntity> query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.Equal ("TestString", "plz");
			List<TestEntity> list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			TestEntity retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.Like ("TestString", "hello");
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			//Or
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.Or ("TestString", "hello", "TestByte", "244");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.Or ("TestString", "hello", "TestNegativeDecimal", "-500");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.Or ("TestString", "hello", "TestByte", "243");
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			//NotEqual
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.NotEqual ("TestString", "hello");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.NotEqual ("TestString", "plz");
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			//GreatherThan
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.GreatherThan ("TestDecimal", "499");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.GreatherThan ("TestDecimal", "500");
			list = DAO.Select (query).Result;
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			//GreatherThanOrEqual
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.GreatherThanOrEqual ("TestDecimal", "500");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.GreatherThanOrEqual ("TestDecimal", "501");
			list = DAO.Select (query).Result;
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			//LessThan
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.LessThan ("TestDecimal", "501");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.LessThan ("Created", DateTime.Now.ToString ("o"));
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.LessThan ("TestDecimal", "500");
			list = DAO.Select (query).Result;
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			//LessThanOrEqual
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.LessThanOrEqual ("TestDecimal", "500");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.LessThanOrEqual ("TestDecimal", "499");
			list = DAO.Select (query).Result;
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			//Like
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.Like ("TestString", "pl");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.Like ("TestString", "hello");
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			//NotLike
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.NotLike ("TestString", "hello");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.NotLike ("TestString", "plz");
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			//Between
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.Between ("TestDecimal", "499", "501");
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived = list [0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.Between ("TestDecimal", "450", "499");
			list = DAO.Select (query).Result;
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);

			/*
			//In
			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.In ("TestList", new string[] {"hello"});
			list = DAO.Select (query).Result;
			Assert.AreEqual (1, list.Count);
			retreived  = list[0];
			AssertTestEntity (retreived);

			query = new SelectQuery<TestEntity> ();
			query.ConsistentRead = true;
			query.In ("TestList", new string[] {"mucho"});
			list = DAO.Select (query).Result;
			list = DAO.Select (query).Result;
			Assert.AreEqual (0, list.Count);
			*/
		}

		[Test ()]
		public void TestGetAll ()
		{
			bool result = DAO.CreateTable ().Result;
			Assert.AreEqual (true, result);

			TestEntity entity = new TestEntity ();
			entity.Id = 0;

			TestEntity entity1 = new TestEntity ();
			entity1.Id = 1;

			bool result3 = DAO.SaveOrReplaceMultiple (new List<TestEntity> (){ entity, entity1 }).Result;
			Assert.AreEqual (true, result3);

			List<TestEntity> list = DAO.GetAll (true).Result;
			Assert.AreEqual (2, list.Count);
		}

		private void AssertTestEntity (TestEntity retreived)
		{
			Assert.AreEqual ("plz", retreived.TestString);
			Assert.AreEqual (false, retreived.TestBool);
			Assert.AreEqual (244, retreived.TestByte);
			Assert.AreEqual (-50, retreived.TestNegativeByte);
			Assert.AreEqual (500, retreived.TestDecimal);
			Assert.AreEqual (-500, retreived.TestNegativeDecimal);
			Assert.AreEqual (new List<string> (new[]{ "hello", "dolly", "the", "sheep" }), retreived.TestList);
		}


	}
}