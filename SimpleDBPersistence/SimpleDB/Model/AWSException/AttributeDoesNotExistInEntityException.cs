using System.Threading.Tasks;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Runtime.InteropServices;
using System;
using System.Xml.Serialization;
using System.Reflection;
using System.Linq;

namespace SimpleDBPersistence.SimpleDB.Model.AWSException
{
	public class AttributeDoesNotExistInEntityException : AWSErrorException
	{
		private readonly string Attribute;

		public AttributeDoesNotExistInEntityException (string attribute)
		{
			this.Attribute = attribute;
		}

		public override string ToString ()
		{
			return string.Format ("[AttributeDoesNotExistInEntityException: Attribute does not exists in the query entity {0}", Attribute);
		}

	}
}

