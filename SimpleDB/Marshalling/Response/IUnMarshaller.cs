using System.Net.Http;
using SimpleDBPersistence.SimpleDB.Response;

namespace SimpleDBPersistence.SimpleDB.Marshalling.Response
{
	public interface IUnMarshaller<T> where T:BaseResponse
	{
		void Configure (HttpResponseMessage message);

		T UnMarshal ();
	}
}

