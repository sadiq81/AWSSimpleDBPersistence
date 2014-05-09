using System.Threading.Tasks;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Runtime.InteropServices;
using System;
using System.Xml.Serialization;
using System.Reflection;
using System.Linq;

namespace AWSSimpleDBPersistence
{
	public class SelectQuery<T> where T : Entity
	{
		public bool ConsistentRead { get; set; }

		public string DomainName { get; set; }

		private string QueryString { get; set; }

		private int NumberOfComparators { get; set; }

		private string SortOrder { get; set; }

		private bool Ascending { get; set; }

		private int Limit{ get; set; }

		public SelectQuery<T> Equal (string attribute, string value)
		{
			CheckIfAttributeExists (attribute);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} = '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} = '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> Or (string attribute1, string value1, string attribute2, string value2)
		{
			CheckIfAttributeExists (attribute1);
			CheckIfAttributeExists (attribute2);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" ({0} = '{1}' or {2} = '{3}')", attribute1, value1, attribute2, value2);
			} else {
				QueryString += string.Format (" and ({0} = '{1}' or {2} = '{3}')", attribute1, value1, attribute2, value2);
			}
			return this;
		}

		public SelectQuery<T> NotEqual (string attribute, string value)
		{
			CheckIfAttributeExists (attribute);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} != '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} != '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> GreatherThan (string attribute, string value)
		{
			CheckIfAttributeExists (attribute);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} > '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} > '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> GreatherThanOrEqual (string attribute, string value)
		{
			CheckIfAttributeExists (attribute);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} >= '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} >= '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> LessThan (string attribute, string value)
		{
			CheckIfAttributeExists (attribute);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} < '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} < '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> LessThanOrEqual (string attribute, string value)
		{
			CheckIfAttributeExists (attribute);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} <= '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} <= '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> Like (string attribute, string value)
		{
			CheckIfAttributeExists (attribute);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} like '{1}%'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} like '{1}%'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> NotLike (string attribute, string value)
		{
			CheckIfAttributeExists (attribute);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} not like '{1}%'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} not like '{1}%'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> Between (string attribute, string lower, string upper)
		{
			CheckIfAttributeExists (attribute);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} between '{1}' and {2}", attribute, lower, upper);
			} else {
				QueryString += string.Format (" and {0} between '{1}' and '{2}'", attribute, lower, upper);
			}
			return this;
		}

		public SelectQuery<T> In (string attribute, string[] values)
		{
			CheckIfAttributeExists (attribute);
			string valuesString = "(";
			foreach (string value in values) {
				valuesString += string.Format ("'{0},'", value);
			}
			valuesString.Remove (valuesString.Length);
			valuesString += ")";

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} in {1}", attribute, valuesString);
			} else {
				QueryString += string.Format (" and {0} in {1}", attribute, valuesString);
			}
			return this;
		}

		private Type CheckIfAttributeExists (string attribute)
		{
			foreach (PropertyInfo propertyInfo in typeof(T).GetRuntimeProperties ().ToList ()) {
				if (0 == string.Compare (propertyInfo.GetCustomAttribute<SimpleDBFieldAttribute> ().Name, attribute, StringComparison.Ordinal)) {
					return propertyInfo.GetType ();
				}
			}
			throw new AttributeDoesNotExistInEntityException ();
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append ("Select * from " + DomainName + " where");
			sb.Append (QueryString);
			sb.Append (" order by " + SortOrder);
			sb.Append (Ascending ? " asc" : " desc");	
			sb.Append (Limit > 0 ? " limit " + Limit : "");		
			return sb.ToString ();
		}
	}
}

