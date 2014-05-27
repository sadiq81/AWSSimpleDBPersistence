using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Net;
using System.Globalization;
using AWSSimpleDBPersistence;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

namespace AWSSimpleDBPersistence
{
	public abstract class GenericDAO<T> : IGenericDAO<T> where T : Entity
	{
		public string GetTableName ()
		{
			T entity = (T)Activator.CreateInstance (typeof(T));
			SimpleDBDomainAttribute attribute = entity.GetType ().GetTypeInfo ().GetCustomAttribute <SimpleDBDomainAttribute> ();
			if (attribute != null) {
				return attribute.Domain;
			} else {
				return entity.GetType ().Name;
			}
		}

		private readonly SimpleDBClientCore Client = ServiceContainer.Resolve<SimpleDBClientCore> ();

		public async Task<bool> CreateTable ()
		{
			CreateDomainRequest request = new CreateDomainRequest ();
			request.DomainName = GetTableName ();
			Response response = await Client.CreateDomain (request);

			//TODO Fails because its a batch attribute response
			if (response.GetType ().Equals (typeof(CreateDomainResponse))) {
				return HttpStatusCode.OK.Equals (response.HttpStatusCode);
			} else {
				throw new AWSErrorException (response);
			}
		}

		public async Task<bool> DeleteTable ()
		{
			DeleteDomainRequest request = new DeleteDomainRequest ();
			request.DomainName = GetTableName ();
			Response response = await Client.DeleteDomain (request);

			//TODO Fails because its a batch attribute response
			if (response.GetType ().Equals (typeof(DeleteDomainResponse))) {
				return HttpStatusCode.OK.Equals (response.HttpStatusCode);
			} else {
				throw new AWSErrorException (response);
			}
		}

		public async Task<bool> SaveOrReplace (T entity)
		{
			if (entity.Created == DateTime.MinValue) {
				entity.Created = DateTime.Now;
			}
			entity.LastUpdated = DateTime.Now;

			PutAttributesRequest request = new PutAttributesRequest ();
			request.DomainName = GetTableName ();
			request.ItemName = entity.Id.ToString ();
			request.ReplaceableAttributes = BuildReplaceableAttributes (entity);

			Response response = await Client.PutAttributes (request);

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
			request.DomainName = GetTableName ();
			request.ItemName = entity.Id.ToString ();

			Response response = await Client.DeleteAttributes (request);

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

			string Domain = GetTableName ();

			Response response;
			BatchDeleteAttributesRequest request;

			bool success = true;

			List<Item> Items = new List<Item> ();

			foreach (T Entity in entities) {
				Item Item = new Item ();
				Item.Name = Entity.Id.ToString ();
				Items.Add (Item);

				if (Items.Count == 25) {
					request = new BatchDeleteAttributesRequest ();
					request.DomainName = Domain;
					request.Items = Items;
					response = await Client.BatchDeleteAttributes (request);
					success = HttpStatusCode.OK.Equals (response.HttpStatusCode) && success;
					Items.Clear ();
				}
			}

			//TODO Undo delete persisted if fails 
			if (Items.Count > 0) {
				request = new BatchDeleteAttributesRequest ();
				request.DomainName = Domain;
				request.Items = Items;
				response = await Client.BatchDeleteAttributes (request);
				success = HttpStatusCode.OK.Equals (response.HttpStatusCode) && success;
			}

			return success;
		}

		public async Task<bool> SaveOrReplaceMultiple (List<T> entities)
		{
			if (entities.Count == 0) {
				return true;
			}

			string Domain = GetTableName ();

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
					response = await Client.BatchPutAttributes (request);
					success = HttpStatusCode.OK.Equals (response.HttpStatusCode) && success;
					ReplaceableItems.Clear ();
				}
			}

			//TODO Delete persisted if fails 
			if (ReplaceableItems.Count > 0) {
				request = new BatchPutAttributesRequest ();
				request.DomainName = Domain;
				request.ReplaceableItems = ReplaceableItems;
				response = await Client.BatchPutAttributes (request);
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

					string value = MarshallField (propertyInfo, attribute, entity);

					ReplaceableAttribute replaceableAttribute = new ReplaceableAttribute (attribute.Name, value, true);
					list.Add (replaceableAttribute);
				}
			}
			return list;
		}

		protected string MarshallField (PropertyInfo propertyInfo, SimpleDBFieldAttribute attribute, T entity)
		{

			string value = "";

			if (typeof(byte).Equals (propertyInfo.PropertyType) ||
			    typeof(ushort).Equals (propertyInfo.PropertyType) ||
			    typeof(uint).Equals (propertyInfo.PropertyType) ||
			    typeof(ulong).Equals (propertyInfo.PropertyType)) {

				value = ApplyPadding (attribute, propertyInfo.GetValue (entity).ToString ());
			} else if (typeof(float).Equals (propertyInfo.PropertyType) ||
			           typeof(double).Equals (propertyInfo.PropertyType) ||
			           typeof(decimal).Equals (propertyInfo.PropertyType) ||
			           typeof(sbyte).Equals (propertyInfo.PropertyType) ||
			           typeof(short).Equals (propertyInfo.PropertyType) ||
			           typeof(int).Equals (propertyInfo.PropertyType) ||
			           typeof(long).Equals (propertyInfo.PropertyType)) {

				value = ApplyOffset (attribute, Decimal.Parse (propertyInfo.GetValue (entity).ToString()));
				value = ApplyPadding (attribute, value);
			} else if (typeof(DateTime).Equals (propertyInfo.PropertyType)) {
				value = ((DateTime)propertyInfo.GetValue (entity)).ToString ("o");
			} else {
				value = Newtonsoft.Json.JsonConvert.SerializeObject (propertyInfo.GetValue (entity));
			}
			return value;
		}

		protected string ApplyOffset (SimpleDBFieldAttribute attribute, Decimal value)
		{

			int offset = attribute.Offset; 

			if (offset > 0) {
				value = value + offset;
				if (value < 0) {
					throw new  FieldFormatException ("Negative value of attribute " + attribute.Name + " is greather than specified offset");
				} 
			}

			return value.ToString ();
		}

		protected string SubstractOffset (SimpleDBFieldAttribute attribute, long value)
		{
			int offset = attribute.Offset; 

			if (offset > 0) {
				value = value - offset;
			}
			return value.ToString ();

		}

		protected string ApplyPadding (SimpleDBFieldAttribute attribute, string value)
		{
			int padding = attribute.ZeroPadding;
			int length = value.Length;
			if (padding > 0) {
				if (length > padding) {
					throw new  FieldFormatException ("String length of value is greather than padding specified in attribute " + attribute.Name);
				} else {
					return value.PadLeft (padding, '0');
				}
			}
			return value;
		}

		protected T MarshallAttributes (Attribute[] attributes)
		{
			T entity = (T)Activator.CreateInstance (typeof(T));

			List<PropertyInfo> propertyInfoList = typeof(T).GetRuntimeProperties ().ToList ();

			Dictionary<string, PropertyInfo> dic = new Dictionary<string, PropertyInfo> ();
			Dictionary<string, SimpleDBFieldAttribute> dic2 = new Dictionary<string, SimpleDBFieldAttribute> ();

			foreach (PropertyInfo propertyInfo in propertyInfoList) {
				SimpleDBFieldAttribute attribute = propertyInfo.GetCustomAttribute<SimpleDBFieldAttribute> ();
				if (attribute != null) {
					dic.Add (attribute.Name, propertyInfo);
					dic2.Add (attribute.Name, attribute);
				}
			}

			foreach (Attribute attribute in attributes) {
				string name = attribute.Name;
				string value = attribute.Value;

				PropertyInfo propertyInfo = dic [name];

				if (typeof(byte).Equals (propertyInfo.PropertyType) ||
				    typeof(ushort).Equals (propertyInfo.PropertyType) ||
				    typeof(uint).Equals (propertyInfo.PropertyType) ||
				    typeof(ulong).Equals (propertyInfo.PropertyType)) {

					propertyInfo.SetValue (entity, ulong.Parse (value));

				} else if (typeof(float).Equals (propertyInfo.PropertyType) ||
				           typeof(double).Equals (propertyInfo.PropertyType) ||
				           typeof(decimal).Equals (propertyInfo.PropertyType) ||
				           typeof(sbyte).Equals (propertyInfo.PropertyType) ||
				           typeof(short).Equals (propertyInfo.PropertyType) ||
				           typeof(int).Equals (propertyInfo.PropertyType) ||
				           typeof(long).Equals (propertyInfo.PropertyType)) {

					propertyInfo.SetValue (entity, SubstractOffset (dic2 [name], long.Parse (value)));

				} else if (typeof(DateTime).Equals (propertyInfo.PropertyType)) {
						
					propertyInfo.SetValue (entity, DateTime.ParseExact (value, "o", CultureInfo.InvariantCulture));

				} else {

					propertyInfo.SetValue (entity, Newtonsoft.Json.JsonConvert.DeserializeObject (value));

				}
			}

			return entity;
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
			Response response = await Client.GetAttributes (request);

			if (response.GetType ().Equals (typeof(GetAttributesResponse))) {
				T entity = MarshallAttributes (((GetAttributesResponse)response).GetAttributesResult);
				entity.Id = id;
				return entity;
			} else {
				throw new AWSErrorException (response);
			}


		}

		public async Task<List<T>> Select (SelectQuery<T> query)
		{
			query.DomainName = GetTableName ();

			List<T> entities = new List<T> ();
			String NextToken = null;

			do {

				SelectRequest request = new SelectRequest ();
				request.SelectExpression = query.ToString ();
				request.ConsistentRead = query.ConsistentRead;
				request.NextToken = NextToken;

				SelectResponse response = await Client.Select (request);

				NextToken = response.SelectResult.NextToken;

				foreach (Item item in response.SelectResult.Item) {
					T entity = MarshallAttributes (item.Attribute);
					entity.Id = long.Parse (item.Name);
					entities.Add (entity);
				}

			} while(NextToken != null);

			return entities;
		}

		public static bool IsNumber (object value)
		{
			return value is sbyte
			|| value is byte
			|| value is short
			|| value is ushort
			|| value is int
			|| value is uint
			|| value is long
			|| value is ulong
			|| value is float
			|| value is double
			|| value is decimal;
		}
	}
}

