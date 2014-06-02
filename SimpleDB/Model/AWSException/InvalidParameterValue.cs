using System;

namespace SimpleDBPersistence.SimpleDB.Model.AWSException
{
	public class InvalidParameterValue : Exception
	{
		public string ErrorMessage {
			get {
				return "Value value for parameter Name is invalid. Value exceeds maximum length of 1024 or \n" +
				"Value value for parameter Value is invalid. Value exceeds maximum length of 1024 or \n" +
				"Value value for parameter Item is invalid. Value exceeds max length of 1024 or \n" +
				"Value value for parameter Replace is invalid. The Replace flag should be either true or false.";
			}
		}
	}
}

