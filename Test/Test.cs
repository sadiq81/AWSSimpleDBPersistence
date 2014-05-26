using NUnit.Framework;
using System;
using AWSSimpleDBPersistence;
using System.Net;
using System.Collections.Generic;

namespace Test
{
	[TestFixture ()]
	public class Test
	{
		TestEntityDAO Dao;
		SimpleDBClientCore Client;

		[TestFixtureSetUp()]
		public void BeforeAllTests(){
			ServiceContainer.Register<ISHA256Service> (() => new SHA256Service ());
			ServiceContainer.Register<SimpleDBClientCore> (() => new SimpleDBClientCore (Properties.AWSAccessKey, Properties.AWSSecretKey, Region.EUWest_1));
			ServiceContainer.Register<TestEntityDAO> (() => new TestEntityDAO ());
			Client = ServiceContainer.Resolve<SimpleDBClientCore>();
			Dao = ServiceContainer.Resolve<TestEntityDAO>();
		}

		[SetUp()]
		public  void BeforeEachTest(){
			CreateDomainResponse response = Client.CreateDomain (new CreateDomainRequest ("Test")).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
		}

		[TearDown()]
		public  void AfterEachTest(){
			DeleteDomainResponse response =  Client.DeleteDomain (new DeleteDomainRequest ("Test")).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
		}

		/*
		 * ListDomains
		 * CreateDomain
		 * DeleteDomain
		 * PutAttributes
		 * GetAttributes
		 * DeleteAttributes
		 * BatchPutAttributes
		 * BatchDeleteAttributes
		 * SelectRequest
		 * 
		 */

		[Test ()]
		public  void TestDomainMetadata (){
			DomainMetadataResponse response =  Client.DomainMetadata (new DomainMetadataRequest ("Test")).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
			Assert.AreEqual ("0", response.DomainMetadataResult.AttributeNameCount);
			Assert.AreEqual ("0", response.DomainMetadataResult.AttributeNamesSizeBytes);
			Assert.AreEqual ("0", response.DomainMetadataResult.AttributeValueCount);
			Assert.AreEqual ("0", response.DomainMetadataResult.AttributeValuesSizeBytes);
		}

		[Test ()]
		public  void TestListDomains (){
			ListDomainsResponse response =  Client.ListDomains (new ListDomainsRequest ()).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
			Assert.AreEqual (1, response.ListDomainsResult.DomainName.GetLength(0));
			Assert.AreSame (null, response.ListDomainsResult.NextToken);
		}

		[Test ()]
		public  void TestPutAttributes (){
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			list.Add (new ReplaceableAttribute ("Test", "Test",true));
			PutAttributesRequest request = new PutAttributesRequest ("Test","0",list);
			PutAttributesResponse response =  Client.PutAttributes (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);
		}

		[Test ()]
		public  void TestGetAttributes (){
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			list.Add (new ReplaceableAttribute ("Test", "Test",true));
			PutAttributesRequest request = new PutAttributesRequest ("Test","0",list);
			PutAttributesResponse response =  Client.PutAttributes (request).Result;
			Assert.AreEqual (HttpStatusCode.OK, response.HttpStatusCode);

			GetAttributesRequest request2 = new GetAttributesRequest("Test","0",true);
			GetAttributesResponse response2 = Client.GetAttributes (request2).Result;
			Assert.AreEqual (HttpStatusCode.OK, response2.HttpStatusCode);

		}


		[TestFixtureTearDown()]
		public void AfterAllTests(){


		}
	}
}

