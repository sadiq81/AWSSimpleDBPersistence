using System;
using SimpleDBPersistence.DAO;
using System.Threading.Tasks;

namespace SimpleDBSample.iOS
{
	public class SampleEntityDAO : GenericDAO<SampleEntity>
	{
		public SampleEntityDAO ()
		{
			Init ();
		}

		private async Task Init ()
		{
			await CreateTable ();
		}
	}
}

