using System;
using System.Net;
using System.Collections.Generic;

namespace SimpleDBPersistence.SimpleDB.Model.Parameters
{
	public class DomainMetadataResult
	{
		public string ItemCount { get; set; }

		public string ItemNamesSizeBytes { get; set; }

		public string AttributeNameCount{ get; set; }

		public string AttributeNamesSizeBytes{ get; set; }

		public string AttributeValueCount{ get; set; }

		public string AttributeValuesSizeBytes{ get; set; }

		public string Timestamp{ get; set; }
	}
}

