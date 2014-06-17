using SimpleDBPersistence.SimpleDB.Model.Parameters;


namespace SimpleDBPersistence.SimpleDB.Response
{
	[System.Xml.Serialization.XmlTypeAttribute (Namespace = "http://sdb.amazonaws.com/doc/2009-04-15/")]
	[System.Xml.Serialization.XmlRootAttribute ("ListDomainsResponse", Namespace = "http://sdb.amazonaws.com/doc/2009-04-15/")]
	public class ListDomainsResponse  : BaseResponse
	{
		public ListDomainsResult ListDomainsResult { get; set; }
	}
}

