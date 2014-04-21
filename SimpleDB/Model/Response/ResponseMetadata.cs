using System;
using System.Collections.Generic;

namespace AWSSimpleDBPersistence
{
	public class ResponseMetadata
	{
		private string requestIdField;
		private IDictionary<string, string> _metadata;

		/// <summary>
		/// Gets and sets the RequestId property.
		/// ID that uniquely identifies a request. Amazon keeps track of request IDs. If you have a question about a request, include the request ID in your correspondence.
		/// </summary>
		public string RequestId {
			get { return this.requestIdField; }
			set { this.requestIdField = value; }
		}

		public IDictionary<string, string> Metadata {
			get {
				if (this._metadata == null)
					this._metadata = new Dictionary<string, string> ();

				return this._metadata;
			}
		}
	}
}

