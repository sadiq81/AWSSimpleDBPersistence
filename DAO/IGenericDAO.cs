using System;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public interface IGenericDAO<T> where T : Entity
	{
		String GetTableName ();
		/*Task<T> Get (T entity);

		Task<T> Get (long id);
		*/
		Task<bool> CreateTable ();

		Task<bool> Delete (T entity);

		Task<bool> DeleteMultiple (List<T> entity);

		Task<bool> SaveOrReplace (T entity);

		Task<bool> SaveOrReplaceMultiple (List<T> entity);
	}
}
