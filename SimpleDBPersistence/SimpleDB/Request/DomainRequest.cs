
namespace SimpleDBPersistence.SimpleDB.Request
{
	public abstract class DomainRequest
	{
		public string DomainName{ get; set; }

		public DomainRequest ()
		{
		}

		public DomainRequest (string domainName)
		{
			this.DomainName = domainName;
		}
	}
}

