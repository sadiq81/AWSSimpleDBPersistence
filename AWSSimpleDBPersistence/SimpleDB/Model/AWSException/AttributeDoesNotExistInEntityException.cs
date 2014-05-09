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
	public class AttributeDoesNotExistInEntityException : Exception
	{
		public override string Message {
			get {
				return "Attribute does not exists in the query entity";
			}
		}
	}
}

