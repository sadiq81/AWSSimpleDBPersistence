﻿using System.Threading.Tasks;
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

		public async Task<Response> ListDomains (ListDomainsRequest request)
		{
			using (Client = new HttpClient ()) {
				ListDomainsRequestMarshaller marshaller = new ListDomainsRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = Client.GetAsync (marshaller.Marshal ()).Result;
				ListDomainsResponseUnMarshaller unmarshaler = new ListDomainsResponseUnMarshaller (responseMessage);
				return unmarshaler.Response;
			}
		}

		public async Task<Response> CreateDomain (CreateDomainRequest request)
		{
			using (Client = new HttpClient ()) {

				CreateDomainRequestMarshaller marshaller = new CreateDomainRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				CreateDomainResponseUnMarshaller unmarshaler = new CreateDomainResponseUnMarshaller (responseMessage);
				return unmarshaler.Response;
			}
		}

		public async Task<Response> DeleteDomain (DeleteDomainRequest request)
		{
			using (Client = new HttpClient ()) {

				DeleteDomainRequestMarshaller marshaller = new DeleteDomainRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				DeleteDomainResponseUnMarshaller unmarshaler = new DeleteDomainResponseUnMarshaller (responseMessage);
				return unmarshaler.Response;
			}

		}

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

		public async Task<Response> GetAttributes (GetAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				GetAttributesRequestMarshaller marshaller = new GetAttributesRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				GetAttributtesResponseUnMarshaller unmarshaler = new GetAttributtesResponseUnMarshaller (responseMessage);
				return unmarshaler.Response;
			}
		}

		public async Task<Response> DeleteAttributes (DeleteAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				DeleteAttributesRequestMarshaller marshaller = new DeleteAttributesRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				DeleteAttributtesResponseUnMarshaller unmarshaler = new DeleteAttributtesResponseUnMarshaller (responseMessage);
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

		public async Task<Response> BatchDeleteAttributes (BatchDeleteAttributesRequest request)
		{
			using (Client = new HttpClient ()) {

				BatchDeleteAttributesRequestMarshaller marshaller = new BatchDeleteAttributesRequestMarshaller ();
				marshaller.Configure (request);
				HttpResponseMessage responseMessage = await Client.GetAsync (marshaller.Marshal ());

				BatchDeleteAttributtesResponseUnMarshaller unmarshaler = new BatchDeleteAttributtesResponseUnMarshaller (responseMessage);
				return unmarshaler.Response;
			}
		}
	}
}

