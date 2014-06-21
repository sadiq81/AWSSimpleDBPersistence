using System;
using Android.App;
using Android.Views;
using Android.OS;
using SimpleDBPersistence.Service;
using SimpleDBSample.Core;
using SimpleDBPersistence.SimpleDB.Model.AWSException;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Widget;
using Android.Util;
using Android.Support.V4.Widget;
using Android.Content;

namespace SimpleDBSample.Droid
{
	[Activity (Label = "SimpleDBSample", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
	public class MainActivity : Activity
	{

		private MainActivityAdapter Adapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource

			SetContentView (Resource.Layout.Main);
			Log.Info ("SimpleDBSample", "Test");
			ListView listView = (ListView)FindViewById (Resource.Id.ListView);
			listView.Adapter = Adapter = new MainActivityAdapter (this);

			SwipeRefreshLayout refresher = FindViewById<SwipeRefreshLayout> (Resource.Id.Refresher);
			refresher.SetColorScheme (Resource.Color.xam_dark_blue,
				Resource.Color.xam_purple,
				Resource.Color.xam_gray,
				Resource.Color.xam_green);
			refresher.Refresh += async delegate {
				await RefreshContent ();
				refresher.Refreshing = false;
			};

			Initialize ();

		}



		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.MainMenu, menu);
			return true;
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.New:
				{
					AddNewItem ();
					return true;
				}
			default:
				{
					return base.OnOptionsItemSelected (item);
				}
			}
		}

		private async void AddNewItem ()
		{
			SampleEntity entity = new SampleEntity ();
			entity.Id = DateTime.Now.Ticks;
			entity.SampleString = DateTime.Now.ToString ("s");

			try {
				await ServiceContainer.Resolve<SampleEntityDAO> ().SaveOrReplace (entity);
				Adapter.Objects.Insert (0, entity);
				Adapter.NotifyDataSetChanged ();
			} catch (AWSErrorException e) {
				ShowAlert (e);
			}
		}

		private async Task RefreshContent ()
		{
			try {
				List<SampleEntity> list = await ServiceContainer.Resolve<SampleEntityDAO> ().GetAll (true);
				Adapter.Objects = list;
				Adapter.NotifyDataSetChanged ();
			} catch (AWSErrorException e) {
				ShowAlert (e);
			}
		}


		private void ShowAlert (AWSErrorException e)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder (this);
			AlertDialog dialog = builder.Create ();
			dialog.SetTitle ("Error");
			dialog.SetIcon (Android.Resource.Drawable.IcDialogAlert);
			dialog.SetMessage (e.ToString ());
			dialog.SetButton ("Ok", (s, ev) => {
			});
			dialog.Show ();

		}

		private  void Initialize ()
		{
			try {
				ServiceContainer.Resolve<SampleEntityDAO> ().CreateTable ();
			} catch (AWSErrorException e) {
				ShowAlert (e);
			}
		}
	}

	public class MainActivityAdapter : BaseAdapter<string>
	{
		private List<SampleEntity> objects = new List<SampleEntity> ();

		public List<SampleEntity> Objects {
			get {
				return objects;
			}
			set {
				objects = value;
			}
		}

		private Activity context;

		public MainActivityAdapter (Activity context) : base ()
		{
			this.context = context;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override string this [int position] {  
			get { return objects [position].SampleString; }
		}

		public override int Count {
			get { return objects.Count; }
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is supplied
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, null);
			// set view properties to reflect data for the given row
			view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = objects [position].SampleString;
			view.Click += (object sender, EventArgs e) => {
				Intent intent = new Intent (context, typeof(DetailActivity));
				intent.PutExtra ("Data", objects [position].SampleString);
				context.StartActivity (intent);


			};
			// return the view, populated with data, for display
			return view;
		}
	}
}


