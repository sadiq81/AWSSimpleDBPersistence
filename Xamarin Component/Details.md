This component helps you in using Amazons SimpleDB database for storing objects.

Instead of using Amazons API which requires you to format your data into a format that Amazons API can accept, 
you can now work with normal objects and store those.


	bool result = DAO.CreateTable ().Result;
	TestEntity entity = new TestEntity ();
	DAO.SaveOrReplace (entity);