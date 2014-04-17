using System;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public interface IGenericDAO<T> where T : Entity
	{
		String GetTableName ();

		Task<bool> Delete (T entity);

		Task<bool> SaveOrReplace (T entity);

		Task<int> SaveOrReplaceMultiple (List<T> entity);
	}
}
