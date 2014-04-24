using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class SimpleDBClient
	{
		public string AWSAccessKeyId { get; set; }

		public string AWSSecretKey { get; set; }

		public string Region { get; set; }

		private HttpClient Client;

		public SimpleDBClient (string aWSAccessKeyId, string aWSSecretKey, string region)
		{
			this.AWSAccessKeyId = aWSAccessKeyId;
			this.AWSSecretKey = aWSSecretKey;
			this.Region = region;
		}
		/*public async Task<BatchDeleteAttributesResponse> BatchDeleteAttributes (BatchDeleteAttributesRequest request)
		{


		}

		public async Task<BatchPutAttributesResponse> BatchPutAttributes (BatchPutAttributesRequest request)
		{

		}

		public async Task<CreateDomainResponse> CreateDomain (CreateDomainRequest request)
		{

		}

		public async Task<DeleteAttributesResponse> DeleteAttributes (DeleteAttributesRequest request)
		{

		}

		public async Task<DeleteDomainResponse> DeleteDomain (DeleteDomainRequest request)
		{

		}

		public async Task<DomainMetadataResponse> DomainMetadata (DomainMetadataRequest request)
		{

		}

		public async Task<GetAttributesResponse> GetAttributes (GetAttributesRequest request)
		{

		}

		public async Task<ListDomainsResponse> ListDomains (ListDomainsRequest request)
		{

		}*/
		public async Task<Response> PutAttributes (PutAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				PutAttributesRequestMarshaller marshaller = new PutAttributesRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				PutAttributtesResponseUnMarshaller unmarshaler = new PutAttributtesResponseUnMarshaller (responseMessage);
				return unmarshaler.Response;
			}
		}

		public async Task<Response> BatchPutAttributes (BatchPutAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				BatchPutAttributesRequestMarshaller marshaller = new BatchPutAttributesRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				BatchPutAttributtesResponseUnMarshaller unmarshaler = new BatchPutAttributtesResponseUnMarshaller (responseMessage);
				return unmarshaler.Response;
			}
		}
	}
}

