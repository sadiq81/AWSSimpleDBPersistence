using Attribute = SimpleDBPersistence.SimpleDB.Model.Parameters.Attribute;

namespace SimpleDBPersistence.SimpleDB.Response
{
	public class GetAttributesResponse : BaseResponse
	{
		public Attribute[] GetAttributesResult { get; set; }
	}
}

