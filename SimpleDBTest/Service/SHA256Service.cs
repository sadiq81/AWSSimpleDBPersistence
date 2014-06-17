using System;
using System.Security.Cryptography;
using System.Text;
using SimpleDBPersistence.Service;

namespace SimpleDBTest
{
	public class SHA256Service : ISHA256Service
	{

		public string CreateHash (string message, string secret)
		{
			byte[] keyByte = Encoding.UTF8.GetBytes (secret);
			byte[] messageBytes = Encoding.UTF8.GetBytes (message);
			using (HMACSHA256 hmacsha256 = new HMACSHA256 (keyByte)) {
				byte[] hashmessage = hmacsha256.ComputeHash (messageBytes);
				return Convert.ToBase64String (hashmessage);
			}
		}
	}
}

