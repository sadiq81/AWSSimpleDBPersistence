using System;
using Android.App;
using Android.Runtime;
using SimpleDBPersistence.Service;
using SimpleDBSample.Core;
using SimpleDBPersistence.SimpleDB;

namespace SimpleDBSample.Droid
{
	[Application]
	public class SimpleDBApplication :Application
	{
		public SimpleDBApplication (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();
			ServiceContainer.Register<ISHA256Service> (() => new SHA256Service ());
			ServiceContainer.Register<SimpleDBClientCore> (() => new SimpleDBClientCore ("KEY", "SECRET", Region.EUWest_1));
			ServiceContainer.Register<SampleEntityDAO> (() => new SampleEntityDAO ());

		}
	}
}

