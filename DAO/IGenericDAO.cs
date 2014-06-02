using System.Threading.Tasks;
using System.Collections.Generic;
using SimpleDBPersistence.Domain;
using SimpleDBPersistence.SimpleDB.Model.Parameters;

namespace SimpleDBPersistence.DAO
{
	public interface IGenericDAO<T> where T : Entity
	{
		Task<T> Get (T entity, bool consistentRead);

		Task<T> Get (long id, bool consistentRead);

		Task<List<T>> GetAll (bool consistentRead);

		Task<bool> CreateTable ();

		Task<bool> DeleteTable ();

		Task<bool> Delete (T entity);

		Task<bool> DeleteMultiple (List<T> entity);

		Task<bool> SaveOrReplace (T entity);

		Task<bool> SaveOrReplaceMultiple (List<T> entity);

		Task<List<T>> Select (SelectQuery<T> query);
	}
}
