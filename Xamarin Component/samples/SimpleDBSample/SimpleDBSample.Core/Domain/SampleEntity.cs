using System;
using SimpleDBPersistence.Domain;
using SimpleDBPersistence.SimpleDB.Model;

namespace SimpleDBSample.Core
{
	[SimpleDBDomain ("SampleEntity")]
	public class SampleEntity : Entity
	{
		[SimpleDBField ("SampleString")]
		public string SampleString { get; set; }

		public SampleEntity ()
		{
		}

		public override string ToString ()
		{
			return string.Format ("[SampleEntity: SampleString={0}]", SampleString);
		}
	}
}

