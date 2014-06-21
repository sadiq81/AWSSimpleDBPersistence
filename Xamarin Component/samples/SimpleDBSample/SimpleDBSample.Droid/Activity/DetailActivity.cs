
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SimpleDBSample.Droid
{
	[Activity (Label = "DetailActivity")]	
	public class DetailActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Detail);
			string text = Intent.GetStringExtra ("Data") ?? "Data not available";
			TextView textView = (TextView)FindViewById (Resource.Id.TextView);
			textView.Text = text;

			ActionBar.SetHomeButtonEnabled (true);
			ActionBar.SetDisplayHomeAsUpEnabled (true);


			// Create your application here
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Android.Resource.Id.Home:
				Finish ();
				return true;

			default:
				return base.OnOptionsItemSelected (item);
			}
		}
	}
}

