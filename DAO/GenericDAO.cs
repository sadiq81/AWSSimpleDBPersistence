using System;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Net;

namespace AWSSimpleDBPersistence
{
	public abstract class GenericDAO<T> : IGenericDAO<T> where T : Entity
	{
		public string GetTableName ()
		{
			SimpleDBDomainAttribute attribute = typeof(T).GetTypeInfo ().GetCustomAttribute <SimpleDBDomainAttribute> ();
			return attribute.Domain;
		}

		AmazonSimpleDBClient client = ServiceContainer.Resolve<AmazonSimpleDBClient> ();

		public async Task<bool> Delete (T entity)
		{
			DeleteAttributesRequest request = new DeleteAttributesRequest ();
			request.DomainName = GetTableName ();
			request.ItemName = entity.Id.ToString ();

			DeleteAttributesResponse response = await client.DeleteAttributesAsync (request);
			return HttpStatusCode.OK.Equals (response.HttpStatusCode);
		}

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

		public async Task<int> SaveOrReplaceMultiple (List<T> entities)
		{
			int count = 0;
			foreach (T entity in entities) {
				if (await SaveOrReplace (entity)) {
					count++;
				}
			}
			return count;
		}

		protected List<ReplaceableAttribute>  BuildPutAttributesRequest (T entity)
		{
			List<ReplaceableAttribute> list = new List<ReplaceableAttribute> ();
			Type type = typeof(T);
			List<PropertyInfo> propertyInfoList = type.GetRuntimeProperties ().ToList ();
			foreach (PropertyInfo info in propertyInfoList) {
				SimpleDBFieldAttribute attribute = info.GetCustomAttribute<SimpleDBFieldAttribute> ();
				if (attribute != null) {
					string name = attribute.Name;
					string value = info.GetValue (entity).ToString ();
					ReplaceableAttribute replaceableAttribute = new ReplaceableAttribute ();
					replaceableAttribute.Name = name;
					replaceableAttribute.Value = value;
					replaceableAttribute.Replace = true;
					list.Add (replaceableAttribute);
				}
			}
			return list;
		}
	}
}

