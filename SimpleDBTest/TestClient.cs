using NUnit.Framework;
using System.Net;
using System.Collections.Generic;
using Attribute = SimpleDBPersistence.SimpleDB.Model.Parameters.Attribute;
using SimpleDBPersistence.Service;
using SimpleDBPersistence.SimpleDB;
using SimpleDBPersistence.SimpleDB.Response;
using SimpleDBPersistence.SimpleDB.Request;
using SimpleDBPersistence.SimpleDB.Model.Parameters;

namespace SimpleDBTest
{
	[TestFixture ()]
	public class TestClient
	{
		SimpleDBClientCore Client;

		[TestFixtureSetUp ()]
		public void BeforeAllTests ()
		{
			SimpleDBPersistence.Service.ServiceContainer.Register<ISHA256Service> (() => new SHA256Service ());
			ServiceContainer.Register<SimpleDBClientCore> (() => new SimpleDBClientCore (Properties.AWSAccessKeyId, Properties.AWSSecretKey, Region.EUWest_1));
			ServiceContainer.Register<TestEntityDAO> (() => new TestEntityDAO ());
			Client = ServiceContainer.Resolve<SimpleDBClientCore> ();
		}

		[SetUp ()]
		public  void BeforeEachTest ()
		{
			CreateDomainResponse response = Client.CreateDomain (new CreateDomainRequest ("Test")).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
		}

		[TearDown ()]
		public  void AfterEachTest ()
		{
			DeleteDomainResponse response = Client.DeleteDomain (new DeleteDomainRequest ("Test")).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
		}

		[Test ()]
		public  void TestDomainMetadata ()
		{
			DomainMetadataResponse response = Client.DomainMetadata (new DomainMetadataRequest ("Test")).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
			Assert.AreEqual ("0", response.DomainMetadataResult.AttributeNameCount);
			Assert.AreEqual ("0", response.DomainMetadataResult.AttributeNamesSizeBytes);
			Assert.AreEqual ("0", response.DomainMetadataResult.AttributeValueCount);
			Assert.AreEqual ("0", response.DomainMetadataResult.AttributeValuesSizeBytes);
		}

		[Test ()]
		public  void TestListDomains ()
		{
			ListDomainsResponse response = Client.ListDomains (new ListDomainsRequest ()).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
			Assert.AreEqual (1, response.ListDomainsResult.DomainName.GetLength (0));
			Assert.AreSame (null, response.ListDomainsResult.NextToken);
		}

		[Test ()]
		public  void TestPutAttributes ()
		{
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			list.Add (new ReplaceableAttribute ("Test", "Test", true));
			PutAttributesRequest request = new PutAttributesRequest ("Test", "0", list);
			PutAttributesResponse response = Client.PutAttributes (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
		}

		[Test ()]
		public  void TestGetAttributes ()
		{
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			list.Add (new ReplaceableAttribute ("Test", "Test", true));
			PutAttributesRequest request = new PutAttributesRequest ("Test", "0", list);
			PutAttributesResponse response = Client.PutAttributes (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);

			GetAttributesRequest request2 = new GetAttributesRequest ("Test", "0", true);
			GetAttributesResponse response2 = Client.GetAttributes (request2).Result;
			Assert.AreEqual (HttpStatusCode.OK, response2.HttpStatusCode);
			Assert.AreEqual ("Test", response2.GetAttributesResult [0].Value);
			Assert.AreEqual ("Test", response2.GetAttributesResult [0].Name);

		}

		[Test ()]
		public  void TestDeleteAttributes ()
		{
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			list.Add (new ReplaceableAttribute ("Test", "Test", true));
			PutAttributesRequest request = new PutAttributesRequest ("Test", "0", list);
			PutAttributesResponse response = Client.PutAttributes (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);

			GetAttributesRequest request2 = new GetAttributesRequest ("Test", "0", true);
			GetAttributesResponse response2 = Client.GetAttributes (request2).Result;
			Assert.AreEqual (HttpStatusCode.OK, response2.HttpStatusCode);
			Assert.AreEqual ("Test", response2.GetAttributesResult [0].Value);
			Assert.AreEqual ("Test", response2.GetAttributesResult [0].Name);

			DeleteAttributesRequest request3 = new DeleteAttributesRequest ("Test", "0");
			DeleteAttributesResponse response3 = Client.DeleteAttributes (request3).Result;
			Assert.AreEqual (HttpStatusCode.OK, response3.HttpStatusCode);

			GetAttributesRequest request4 = new GetAttributesRequest ("Test", "0", true);
			GetAttributesResponse response4 = Client.GetAttributes (request4).Result;
			Assert.AreEqual (HttpStatusCode.OK, response4.HttpStatusCode);
			Assert.AreEqual (0, response4.GetAttributesResult.Length);
		}

		[Test ()]
		public  void TestBatchPutAttributes ()
		{
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			list.Add (new ReplaceableAttribute ("Test", "Test", true));

			List<ReplaceableItem> items = new List<ReplaceableItem> ();
			ReplaceableItem item0 = new ReplaceableItem ("0", list);
			items.Add (item0);
			ReplaceableItem item1 = new ReplaceableItem ("1", list);
			items.Add (item1);

			BatchPutAttributesRequest request = new BatchPutAttributesRequest ("Test", items);
			BatchPutAttributesResponse response = Client.BatchPutAttributes (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
		}

		[Test ()]
		public  void TestBatchDeleteAttributes ()
		{
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			list.Add (new ReplaceableAttribute ("Test", "Test", true));

			List<ReplaceableItem> items = new List<ReplaceableItem> ();
			ReplaceableItem item0 = new ReplaceableItem ("0", list);
			items.Add (item0);
			ReplaceableItem item1 = new ReplaceableItem ("1", list);
			items.Add (item1);

			BatchPutAttributesRequest request = new BatchPutAttributesRequest ("Test", items);
			BatchPutAttributesResponse response = Client.BatchPutAttributes (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);

			List<Attribute> list2 = new List<Attribute> ();
			list2.Add (new Attribute ("Test", "Test"));
			List<Item> items2 = new List<Item> ();
			items2.Add (new Item ("0", list2.ToArray ()));
			items2.Add (new Item ("1", list2.ToArray ()));


			BatchDeleteAttributesRequest request2 = new BatchDeleteAttributesRequest ("Test", items2);
			BatchDeleteAttributesResponse response2 = Client.BatchDeleteAttributes (request2).Result;
			Assert.AreEqual (HttpStatusCode.OK, response2.HttpStatusCode);
		}

		[Test ()]
		public void TestSelectRequest ()
		{
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			list.Add (new ReplaceableAttribute ("Test", "true", true));

			List<ReplaceableItem> items = new List<ReplaceableItem> ();
			ReplaceableItem item0 = new ReplaceableItem ("0", list);
			items.Add (item0);
			ReplaceableItem item1 = new ReplaceableItem ("1", list);
			items.Add (item1);
			BatchPutAttributesRequest request = new BatchPutAttributesRequest ("Test", items);
			BatchPutAttributesResponse response = Client.BatchPutAttributes (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);

			SelectRequest request2 = new SelectRequest ("select * from Test where Test = 'true'", true);
			SelectResponse response2 = Client.Select (request2).Result;

			Assert.AreEqual ("Test", response2.SelectResult.Item [0].Attribute [0].Name);
			Assert.AreEqual ("true", response2.SelectResult.Item [0].Attribute [0].Value);
			Assert.AreEqual ("Test", response2.SelectResult.Item [1].Attribute [0].Name);
			Assert.AreEqual ("true", response2.SelectResult.Item [1].Attribute [0].Value);
			Assert.AreEqual (HttpStatusCode.OK, response2.HttpStatusCode);

		}


		[TestFixtureTearDown ()]
		public void AfterAllTests ()
		{


		}
	}
}

