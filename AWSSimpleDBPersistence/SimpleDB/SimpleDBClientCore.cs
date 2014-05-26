using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System;

namespace AWSSimpleDBPersistence
{
	public  class SimpleDBClientCore
	{
		public string AWSAccessKeyId { get; set; }

		public string AWSSecretKey { get; set; }

		public string Region { get; set; }

		private HttpClient Client;

		public SimpleDBClientCore (string aWSAccessKeyId, string aWSSecretKey, string region)
		{
			this.AWSAccessKeyId = aWSAccessKeyId;
			this.AWSSecretKey = aWSSecretKey;
			this.Region = region;
		}

		public async Task<CreateDomainResponse> CreateDomain (CreateDomainRequest request)
		{
			using (Client = new HttpClient ()) {

				CreateDomainRequestMarshaller marshaller = new CreateDomainRequestMarshaller ();
				marshaller.Configure (request);

				HttpResponseMessage responseMessage =  await Client.SendAsync (ConfigureClient(Client, marshaller));

				CreateDomainResponseUnMarshaller unmarshaler = new CreateDomainResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();
			}
		}

		public async Task<DeleteDomainResponse> DeleteDomain (DeleteDomainRequest request)
		{
			using (Client = new HttpClient ()) {

				DeleteDomainRequestMarshaller marshaller = new DeleteDomainRequestMarshaller ();
				marshaller.Configure (request);

				HttpResponseMessage responseMessage = await Client.SendAsync (ConfigureClient(Client,marshaller));

				DeleteDomainResponseUnMarshaller unmarshaler = new DeleteDomainResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();

			}

		}

		public async Task<DomainMetadataResponse> DomainMetadata (DomainMetadataRequest request)
		{
			using (Client = new HttpClient ()) {
				DomainMetadataRequestMarshaller marshaller = new DomainMetadataRequestMarshaller ();
				marshaller.Configure (request);

				HttpResponseMessage responseMessage = await Client.SendAsync (ConfigureClient(Client,marshaller));

				DomainMetadataResponseUnMarshaller unmarshaler = new DomainMetadataResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();
			}
		}

		public async Task<ListDomainsResponse> ListDomains (ListDomainsRequest request)
		{
			using (Client = new HttpClient ()) {
				ListDomainsRequestMarshaller marshaller = new ListDomainsRequestMarshaller ();
				marshaller.Configure (request);

				HttpResponseMessage responseMessage = await Client.SendAsync (ConfigureClient(Client,marshaller));

				ListDomainsResponseUnMarshaller unmarshaler = new ListDomainsResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();
			}
		}

		public async Task<PutAttributesResponse> PutAttributes (PutAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				PutAttributesRequestMarshaller marshaller = new PutAttributesRequestMarshaller ();
				marshaller.Configure (request);
		
				HttpResponseMessage responseMessage = await Client.SendAsync (ConfigureClient(Client,marshaller));

				PutAttributtesResponseUnMarshaller unmarshaler = new PutAttributtesResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();

			}
		}

		private HttpRequestMessage ConfigureClient(HttpClient client, BaseMarshaller marshaller){

			client.BaseAddress = new Uri("https://" + Region);
			HttpRequestMessage requestMessage = new HttpRequestMessage (HttpMethod.Post,"");
			requestMessage.Content = new FormUrlEncodedContent (marshaller.MarshallPost());
			requestMessage.Content.Headers.ContentType.CharSet = "utf-8";
			return requestMessage;
		}



		//----------------------------------------------------------------------------------------------------------------------------





		public async Task<GetAttributesResponse> GetAttributes (GetAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				GetAttributesRequestMarshaller marshaller = new GetAttributesRequestMarshaller ();
				marshaller.Configure (request);

				HttpResponseMessage responseMessage = await Client.SendAsync (ConfigureClient(Client,marshaller));

				GetAttributtesResponseUnMarshaller unmarshaler = new GetAttributtesResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();
			}
		}

		public async Task<DeleteAttributesResponse> DeleteAttributes (DeleteAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				DeleteAttributesRequestMarshaller marshaller = new DeleteAttributesRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				DeleteAttributtesResponseUnMarshaller unmarshaler = new DeleteAttributtesResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();
			}

		}

		public async Task<BatchPutAttributesResponse> BatchPutAttributes (BatchPutAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				BatchPutAttributesRequestMarshaller marshaller = new BatchPutAttributesRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				BatchPutAttributtesResponseUnMarshaller unmarshaler = new BatchPutAttributtesResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();
			}
		}

		public async Task<BatchDeleteAttributesResponse> BatchDeleteAttributes (BatchDeleteAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				BatchDeleteAttributesRequestMarshaller marshaller = new BatchDeleteAttributesRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				BatchDeleteAttributtesResponseUnMarshaller unmarshaler = new BatchDeleteAttributtesResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();
			}
		}

		public async Task<SelectResponse> Select (SelectRequest request)
		{
			using (Client = new HttpClient ()) {

				SelectRequestMarshaller marshaller = new SelectRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				SelectResponseUnMarshaller unmarshaler = new SelectResponseUnMarshaller ();
				unmarshaler.Configure (responseMessage);
				return unmarshaler.UnMarshal ();
			}
		}
	}
}

