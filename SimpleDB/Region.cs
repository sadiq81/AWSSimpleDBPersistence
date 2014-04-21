using System;
using System.Dynamic;

namespace AWSSimpleDBPersistence
{
	public static class Region
	{
		public static string USEast_1 {
			get {
				return "sdb.amazonaws.com";
			}
		}

		public static string USWest_2 {
			get {
				return "sdb.us-west-2.amazonaws.com";
			}
		}

		public static string USWest_1 {
			get {
				return "sdb.us-west-1.amazonaws.com";
			}
		}

		public static string EUWest_1 {
			get {
				return "sdb.eu-west-1.amazonaws.com";
			}
		}

		public static string APSouthEast_1 {
			get {
				return "sdb.ap-southeast-1.amazonaws.com";
			}
		}

		public static string APSouthEast_2 {
			get {
				return "sdb.ap-southeast-2.amazonaws.com";
			}
		}

		public static string APNorthEast_1 {
			get {
				return "sdb.ap-northeast-1.amazonaws.com";
			}
		}

		public static string SAEast_1 {
			get {
				return "sdb.sa-east-1.amazonaws.com";
			}
		}
	}
}

