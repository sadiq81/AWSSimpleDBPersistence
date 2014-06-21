using System;
using SimpleDBPersistence.Service;
using System.Text;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;


namespace SimpleDBPersistence.Service
{
	public class SHA256Service : ISHA256Service
	{
		public SHA256Service ()
		{
		}

		public string CreateHash (string message, string secret)
		{
			byte[] keyByte = Encoding.UTF8.GetBytes (secret);
			byte[] messageBytes = Encoding.UTF8.GetBytes (message);


			HMac mac = new HMac (new Sha256Digest ());
			byte[] resBuf = new byte[mac.GetMacSize ()];

			mac.Init (new KeyParameter (keyByte));
			mac.Update ();



			/*
			using (HMACSHA256 hmacsha256 = new HMACSHA256 (keyByte)) {
				byte[] hashmessage = hmacsha256.ComputeHash (messageBytes);
				return Convert.ToBase64String (hashmessage);
			}
			*/
			return "";
		}

	}
}

