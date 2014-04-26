AWSSimpleDBPersistence
======================

CURRENTLY UNDER DEVELOPMENT

Persistence framework for Amazons SimpleDB framework in C#. This is a complete rewrite with inspiration from the Amazon .NET SDK but designed to work in Xamarin mobile applications.

Setup:

Before programstart the ServiceContainer must be initialized with an AmazonSimpleDBClient

For example like this:

    ServiceContainer.Register<AmazonSimpleDBClient> (() => new AmazonSimpleDBClient ("AWSAccessKey", "AWSSecretKey", RegionEndpoint.EUWest1));

Then add Attributes to Entities like so

    using System;
    using AWSSimpleDBPersistence;
    
    namespace Test
    {
    	[SimpleDBDomainAttribute ("test")]
    	public class Transaction : Entity
    	{
    		[SimpleDBFieldAttribute ("test")]
    		public string Test { get; set; }
    
    		public Transaction ()
    		{
    		}
    	}
    }
    
Save Entities like so:

    Transaction transaction = new Transaction ();
    transaction.Test = "test";
    
    TransactionDAO dao = new TransactionDAO ();
    dao.SaveOrReplace (transaction);

