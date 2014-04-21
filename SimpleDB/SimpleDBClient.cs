using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

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
		public async Task<PutAttributesResponse> PutAttributes (PutAttributesRequest request)
		{
			BatchPutAttributesRequest batchRequest = new BatchPutAttributesRequest (request);
			await BatchPutAttributes (batchRequest);

			return new PutAttributesResponse ();
		}

		public async Task<BatchPutAttributesResponse> BatchPutAttributes (BatchPutAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				BatchPutAttributesRequestMarshaller marshaller = new BatchPutAttributesRequestMarshaller (request);

				var response = Client.GetAsync (marshaller.Marshal ());

				if (response.Result.IsSuccessStatusCode) {
					// by calling .Result you are performing a synchronous call
					var responseContent = response.Result.Content; 

					// by calling .Result you are synchronously reading the result
					string responseString = responseContent.ReadAsStringAsync ().Result;

					Debug.WriteLine (responseString);
				}

			}
			return new BatchPutAttributesResponse ();
		}
		/*		public async Task<SelectResponse> Select (SelectRequest request)
		{

		}
		*/
	}
}

