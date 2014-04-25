using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Net;
using System.Globalization;
using AWSSimpleDBPersistence;
using System.Xml.Linq;

namespace AWSSimpleDBPersistence
{
	public abstract class GenericDAO<T> : IGenericDAO<T> where T : Entity
	{
		const string FMT = "yyyy-MM-dd-HH-mm-ss";

		public string GetTableName (T entity)
		{

			SimpleDBDomainAttribute attribute = entity.GetType ().GetTypeInfo ().GetCustomAttribute <SimpleDBDomainAttribute> ();
			return attribute.Domain;
		}

		SimpleDBClient client = ServiceContainer.Resolve<SimpleDBClient> ();

		public async Task<bool> SaveOrReplace (T entity)
		{
			if (entity.Created == DateTime.MinValue) {
				entity.Created = DateTime.Now;
			}
			entity.LastUpdated = DateTime.Now;

			PutAttributesRequest request = new PutAttributesRequest ();
			request.DomainName = GetTableName (entity);
			request.ItemName = entity.Id.ToString ();
			request.ReplaceableAttributes = BuildReplaceableAttributes (entity);

			Response response = await client.PutAttributes (request);

			//TODO Fails because its a batch attribute response
			if (response.GetType ().Equals (typeof(PutAttributesResponse))) {
				return HttpStatusCode.OK.Equals (response.HttpStatusCode);
			} else {
				throw new AWSErrorException (response);
			}
		}

		public async Task<bool> Delete (T entity)
		{
			DeleteAttributesRequest request = new DeleteAttributesRequest ();
			request.DomainName = GetTableName (entity);
			request.ItemName = entity.Id.ToString ();

			Response response = await client.DeleteAttributes (request);

			if (response.GetType ().Equals (typeof(DeleteAttributesResponse))) {
				return HttpStatusCode.OK.Equals (response.HttpStatusCode);
			} else {
				throw new AWSErrorException (response);
			}

		}

		public async Task<bool> DeleteMultiple (List<T> entities)
		{
			if (entities.Count == 0) {
				return true;
			}

			string Domain = GetTableName (entities [0]);

			Response response;
			BatchDeleteAttributesRequest request;

			bool success = true;

			List<Item> Items = new List<Item> ();

			foreach (T Entity in entities) {
				Item Item = new Item ();
				Item.ItemName = Entity.Id.ToString ();
				Items.Add (Item);

				if (Items.Count == 25) {
					request = new BatchDeleteAttributesRequest ();
					request.DomainName = Domain;
					request.Items = Items;
					response = await client.BatchDeleteAttributes (request);
					success = HttpStatusCode.OK.Equals (response.HttpStatusCode) && success;
					Items.Clear ();
				}
			}

			//TODO Undo delete persisted if fails 
			if (Items.Count > 0) {
				request = new BatchDeleteAttributesRequest ();
				request.DomainName = Domain;
				request.Items = Items;
				response = await client.BatchDeleteAttributes (request);
				success = HttpStatusCode.OK.Equals (response.HttpStatusCode) && success;
			}

			return success;
		}

		public async Task<bool> SaveOrReplaceMultiple (List<T> entities)
		{
			if (entities.Count == 0) {
				return true;
			}

			string Domain = GetTableName (entities [0]);

			foreach (Entity entity in entities) {
				if (entity.Created == DateTime.MinValue) {
					entity.Created = DateTime.Now;
				}
				entity.LastUpdated = DateTime.Now;
			}

			Response response;
			BatchPutAttributesRequest request;

			bool success = true;

			List<ReplaceableItem> ReplaceableItems = new List<ReplaceableItem> ();

			foreach (T Entity in entities) {
				ReplaceableItem Item = new ReplaceableItem ();
				Item.ItemName = Entity.Id.ToString ();
				Item.ReplaceableAttributes = BuildReplaceableAttributes (Entity);
				ReplaceableItems.Add (Item);

				if (ReplaceableItems.Count == 25) {
					request = new BatchPutAttributesRequest ();
					request.DomainName = Domain;
					request.ReplaceableItems = ReplaceableItems;
					response = await client.BatchPutAttributes (request);
					success = HttpStatusCode.OK.Equals (response.HttpStatusCode) && success;
					ReplaceableItems.Clear ();
				}
			}

			//TODO Delete persisted if fails 
			if (ReplaceableItems.Count > 0) {
				request = new BatchPutAttributesRequest ();
				request.DomainName = Domain;
				request.ReplaceableItems = ReplaceableItems;
				response = await client.BatchPutAttributes (request);
				success = HttpStatusCode.OK.Equals (response.HttpStatusCode) && success;
			}

			return success;
		}

		protected List<ReplaceableAttribute>  BuildReplaceableAttributes (T entity)
		{
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			List<PropertyInfo> propertyInfoList = typeof(T).GetRuntimeProperties ().ToList ();

			foreach (PropertyInfo propertyInfo in propertyInfoList) {
				SimpleDBFieldAttribute attribute = propertyInfo.GetCustomAttribute<SimpleDBFieldAttribute> ();
				if (attribute != null) {

					string name = attribute.Name;
					string value = "";

					if (typeof(string).Equals (propertyInfo.PropertyType)) {
						value = (string)propertyInfo.GetValue (entity);
					} else if (typeof(DateTime).Equals (propertyInfo.PropertyType)) {
						value = ((DateTime)propertyInfo.GetValue (entity)).ToString (FMT);
					} else {
						throw new NullReferenceException ("Unknown type being parsed" + propertyInfo.PropertyType.ToString ());
					}

					ReplaceableAttribute replaceableAttribute = new ReplaceableAttribute (name, value, true);
					list.Add (replaceableAttribute);
				}
			}
			return list;
		}

		protected T MarshallAttributes (List<ReplaceableAttribute> attributes)
		{
			T entity = (T)Activator.CreateInstance (typeof(T));

			List<PropertyInfo> propertyInfoList = typeof(T).GetRuntimeProperties ().ToList ();

			Dictionary<string, PropertyInfo> dic = new Dictionary<string, PropertyInfo> ();

			foreach (PropertyInfo propertyInfo in propertyInfoList) {
				SimpleDBFieldAttribute attribute = propertyInfo.GetCustomAttribute<SimpleDBFieldAttribute> ();
				if (attribute != null) {
					dic.Add (attribute.Name, propertyInfo);
				}
			}

			foreach (ReplaceableAttribute attribute in attributes) {
				string name = attribute.Name;
				string value = attribute.Value;

				PropertyInfo propertyInfo = dic [name];

				if (typeof(string).Equals (propertyInfo.PropertyType)) {
					propertyInfo.SetValue (entity, value);
				} else if (typeof(DateTime).Equals (propertyInfo.PropertyType)) {
					DateTime time = DateTime.ParseExact (value, FMT, CultureInfo.InvariantCulture);
					propertyInfo.SetValue (entity, time);
				} else {
					throw new NullReferenceException ("Unknown type being parsed" + propertyInfo.PropertyType.ToString ());
				}
			}
			return entity;
		}
		/*
		public async Task<T> Get (T entity)
		{
			return  await Get (entity.Id);
		}

		public async Task<T> Get (long id)
		{
			GetAttributesRequest request = new GetAttributesRequest ();
			request.DomainName = GetTableName ();
			request.ItemName = id.ToString ();
			request.ConsistentRead = true;
			GetAttributesResponse response = await client.GetAttributesAsync (request);
			T entity = MarshallAttributes (response.Attributes);
			entity.Id = id;
			return entity;
		}
			*/
	}
}

