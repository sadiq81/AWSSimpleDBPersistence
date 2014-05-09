using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace AWSSimpleDBPersistence
{
	public interface IUnMarshaller<T> where T:Response
	{
		void Configure (HttpResponseMessage message);

		T UnMarshal ();
	}
}

