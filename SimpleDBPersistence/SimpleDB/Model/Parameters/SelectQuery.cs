using System.Text;
using System;
using System.Reflection;
using System.Linq;
using SimpleDBPersistence.Domain;
using SimpleDBPersistence.SimpleDB.Model.AWSException;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleDBPersistence.SimpleDB.Model.Parameters
{
	public class SelectQuery<T> where T : Entity
	{
		public bool ConsistentRead { get; set; }

		public string DomainName { get; set; }

		private string QueryString = "";

		private int NumberOfComparators { get; set; }

		public string SortOrder { get; set; }

		private bool Ascending { get; set; }

		public int Limit{ get; set; }

		public bool GetAll{ get; set; }

		public bool Count{ get; set; }

		public SelectQuery<T> Equal (string attribute, string value)
		{

			value = ApplyAttributes (attribute, value);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} = '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} = '{1}'", attribute, value);
			}
			return this;
		}

		//TODO Create new TestCASE
		public SelectQuery<T> Or (string attribute, string[] values)
		{
			string[] conditions = new string[values.Length * 2];

			for (int i = 0; i < values.Length * 2; i += 2) {
				conditions [i] = attribute;
				conditions [i + 1] = values [i / 2];
			}

			return Or (conditions);
		}


		//TODO Create new TestCASE
		public SelectQuery<T> Or (string[] conditions)
		{
			if (conditions.Length < 2 || conditions.Length % 2 != 0) {
				throw new ArgumentException ("Must be at least to coditions and there must equal many attributes and values");
			}

			List<string> attributes = new List<string> ();
			List<string> values = new List<string> ();

			for (int i = 0; i < conditions.Length; i += 2) {
				attributes.Add (conditions [i]);
				values.Add (ApplyAttributes (conditions [i], conditions [i + 1]));
			}

			if (QueryString.Length != 0) {

				QueryString += " and ";

			} 

			QueryString += "(";

			for (int i = 0; i < attributes.Count; i++) {
				QueryString += string.Format (" {0} = '{1}' or", attributes [i], values [i]);
			}

			QueryString = QueryString.Substring (0, QueryString.Length - 3);

			QueryString += ")";

			return this;
		}

		public SelectQuery<T> NotEqual (string attribute, string value)
		{
			value = ApplyAttributes (attribute, value);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} != '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} != '{1}'", attribute, value);
			}
			return this;
		}


		public SelectQuery<T> GreatherThan (string attribute, string value)
		{
			value = ApplyAttributes (attribute, value);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} > '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} > '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> GreatherThanOrEqual (string attribute, string value)
		{
			value = ApplyAttributes (attribute, value);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} >= '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} >= '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> LessThan (string attribute, string value)
		{
			value = ApplyAttributes (attribute, value);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} < '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} < '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> LessThanOrEqual (string attribute, string value)
		{
			value = ApplyAttributes (attribute, value);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} <= '{1}'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} <= '{1}'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> Like (string attribute, string value)
		{
			value = ApplyAttributes (attribute, value);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} like '{1}%'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} like '{1}%'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> NotLike (string attribute, string value)
		{
			value = ApplyAttributes (attribute, value);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} not like '{1}%'", attribute, value);
			} else {
				QueryString += string.Format (" and {0} not like '{1}%'", attribute, value);
			}
			return this;
		}

		public SelectQuery<T> NotNull (string attribute)
		{
			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} is not null", attribute);
			} else {
				QueryString += string.Format (" and {0} is not null", attribute);
			}
			return this;
		}

		public SelectQuery<T> Between (string attribute, string lower, string upper)
		{
			lower = ApplyAttributes (attribute, lower);
			upper = ApplyAttributes (attribute, upper);

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {0} between '{1}' and '{2}'", attribute, lower, upper);
			} else {
				QueryString += string.Format (" and {0} between '{1}' and '{2}'", attribute, lower, upper);
			}
			return this;
		}

		/*
		public SelectQuery<T> In (string attribute, string[] values)
		{
			for (int i = 0; i < values.Length; i++){
				values[i] = ApplyAttributes (attribute, values[i]);
			}

			string valuesString = "(";
			foreach (string value in values) {
				valuesString += string.Format ("'{0}',", value);
			}
			valuesString = valuesString.Remove (valuesString.Length-1);
			valuesString += ")";

			if (QueryString.Length == 0) {
				QueryString += string.Format (" {1} in {0}", attribute, valuesString);
			} else {
				QueryString += string.Format (" and {1} in {0}", attribute, valuesString);
			}
			return this;
		}
		*/

		private string ApplyAttributes (string attribute, string value)
		{
			string checkedValue = value;
			PropertyInfo propertyInfo = CheckIfAttributeExists (attribute);
			SimpleDBFieldAttribute attributes = propertyInfo.GetCustomAttribute<SimpleDBFieldAttribute> ();
			if (attributes.Offset > 0) {
				checkedValue = SimpleDBFieldAttribute.ApplyOffset (attributes, decimal.Parse (value));
			}
			if (attributes.ZeroPadding > 0) {
				checkedValue = SimpleDBFieldAttribute.ApplyPadding (attributes, checkedValue);
			}
			return checkedValue;
		}

		private PropertyInfo CheckIfAttributeExists (string attribute)
		{
			foreach (PropertyInfo propertyInfo in typeof(T).GetRuntimeProperties ().ToList ()) {
				SimpleDBFieldAttribute att = propertyInfo.GetCustomAttribute<SimpleDBFieldAttribute> ();
				if (att != null && 0 == string.Compare (att.Name, attribute, StringComparison.Ordinal)) {
					return propertyInfo;
				}
			}
			throw new AttributeDoesNotExistInEntityException (attribute);
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append ("Select * from " + DomainName);
			if (GetAll && !Count) {
				return sb.ToString ();
			}
			if (Count) {
				sb.Replace ("*", "count(*)");
			}
			if (QueryString.Length != 0) {
				sb.Append (" where");
				sb.Append (QueryString);
			}

			if (SortOrder != null) {
				sb.Append (" order by " + SortOrder);
				sb.Append (Ascending ? " asc" : " desc");	
			}

			sb.Append (Limit > 0 ? " limit " + Limit : "");		

			return sb.ToString ();
		}
	}
}

