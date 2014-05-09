using System;

namespace AWSSimpleDBPersistence
{
	public interface ITypeMarshaller<T>
	{
		string Marshall (T type, SimpleDBFieldAttribute fieldAtttribute);

		T UnMarshall (string marshalled);
	}
}



