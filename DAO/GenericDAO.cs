using System;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Net;
using System.Globalization;

namespace AWSSimpleDBPersistence
{
	public abstract class GenericDAO<T> : IGenericDAO<T> where T : Entity
	{
		const string FMT = "yyyy-MM-dd HH:mm:ss.fff";

		public string GetTableName ()
		{
			SimpleDBDomainAttribute attribute = typeof(T).GetTypeInfo ().GetCustomAttribute <SimpleDBDomainAttribute> ();
			return attribute.Domain;
		}

		AmazonSimpleDBClient client = ServiceContainer.Resolve<AmazonSimpleDBClient> ();

		public async Task<bool> SaveOrReplace (T entity)
		{
			PutAttributesRequest request = new PutAttributesRequest ();
			request.DomainName = GetTableName ();
			request.ItemName = entity.Id.ToString ();
			if (entity.Created == DateTime.MinValue) {
				entity.Created = DateTime.Now;
			}
			entity.LastUpdated = DateTime.Now;
			request.Attributes = BuildPutAttributesRequest (entity);

			PutAttributesResponse response = await client.PutAttributesAsync (request);

			return HttpStatusCode.OK.Equals (response.HttpStatusCode);
		}

		public async Task<bool> SaveOrReplaceMultiple (List<T> entities)
		{
		
			BatchPutAttributesRequest request;
			BatchPutAttributesResponse response;
			bool success = true;

			List<ReplaceableItem> ReplaceableItems = new List<ReplaceableItem> ();
			foreach (T Entity in entities) {
				ReplaceableItem Item = new ReplaceableItem ();
				Item.Name = Entity.Id.ToString ();
				Item.Attributes = BuildPutAttributesRequest (Entity);
				ReplaceableItems.Add (Item);

				if (ReplaceableItems.Count == 25) {
					request = new BatchPutAttributesRequest ();
					request.DomainName = GetTableName ();
					request.Items = ReplaceableItems;
					response = await client.BatchPutAttributesAsync (request);
					success = HttpStatusCode.OK.Equals (response.HttpStatusCode) && success;
					ReplaceableItems.Clear ();
				}
			}

			if (ReplaceableItems.Count > 0) {
				request = new BatchPutAttributesRequest ();
				request.DomainName = GetTableName ();
				request.Items = ReplaceableItems;
				response = await client.BatchPutAttributesAsync (request);
				success = HttpStatusCode.OK.Equals (response.HttpStatusCode) && success;
			}

			return success;
		}

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

		public async Task<bool> Delete (T entity)
		{
			DeleteAttributesRequest request = new DeleteAttributesRequest ();
			request.DomainName = GetTableName ();
			request.ItemName = entity.Id.ToString ();
			DeleteAttributesResponse response = await client.DeleteAttributesAsync (request);
			return HttpStatusCode.OK.Equals (response.HttpStatusCode);
		}

		protected List<ReplaceableAttribute>  BuildPutAttributesRequest (T entity)
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

					ReplaceableAttribute replaceableAttribute = new ReplaceableAttribute ();
					replaceableAttribute.Name = name;
					replaceableAttribute.Value = value;
					replaceableAttribute.Replace = true;
					list.Add (replaceableAttribute);
				}
			}
			return list;
		}

		protected T MarshallAttributes (List<Amazon.SimpleDB.Model.Attribute> attributes)
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

			foreach (Amazon.SimpleDB.Model.Attribute attribute in attributes) {
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
	}
}

