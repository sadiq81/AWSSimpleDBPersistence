using System;
using PCLCrypto;
using System.Text;

namespace SimpleDBPersistence.Service
{
	public class SHA256ServicePCL
	{
		public SHA256ServicePCL ()
		{
		}

		public byte[] CreateHash (byte[] key, byte[] buffer)
		{
			var algorithm = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha256);
			CryptographicHash hasher = algorithm.CreateHash(key);
			hasher.Append(buffer);
			byte[] mac = hasher.GetValueAndReset();
			return mac;
		
		}

		public byte[] CreateHash (byte[] buffer)
		{
			var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha256);
			byte[] hash = hasher.HashData(buffer);
			return hash;
		}

		public string CreateHash (string message, string secret)
		{
			return Convert.ToBase64String (CreateHash (Encoding.UTF8.GetBytes (secret), Encoding.UTF8.GetBytes (message)));
		}
	}
}

