using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimpleDBPersistence.Service;
using SimpleDBPersistence.SimpleDB;
using SimpleDBSample.Core;

namespace SimpleDBSample.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.

			ServiceContainer.Register<ISHA256Service> (() => new SHA256Service ());
			ServiceContainer.Register<SimpleDBClientCore> (() => new SimpleDBClientCore ("AKIAJCSIBUKJK42X5XLA", "de2Rssuss5rQrWLPbTvyEdQa6QaIrfmnnLMV8wY0", Region.EUWest_1));
			ServiceContainer.Register<SampleEntityDAO> (() => new SampleEntityDAO ());

			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
