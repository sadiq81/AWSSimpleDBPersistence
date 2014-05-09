using System;
using System.Threading.Tasks;

namespace AWSSimpleDBPersistence
{
	public abstract class BaseTypeMarshaller<T> : ITypeMarshaller<T>
	{
		public string Marshall (T type, SimpleDBFieldAttribute fieldAtttribute)
		{
			throw new NotImplementedException ();
		}

		public T UnMarshall (string marshalled)
		{
			throw new NotImplementedException ();
		}
	}
}

