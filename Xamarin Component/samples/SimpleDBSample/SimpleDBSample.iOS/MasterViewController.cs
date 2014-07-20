using System;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimpleDBPersistence.Service;
using System.Threading.Tasks;
using SimpleDBPersistence.SimpleDB.Model.AWSException;
using SimpleDBSample.Core;

namespace SimpleDBSample.iOS
{
	public partial class MasterViewController : UITableViewController
	{
		DataSource dataSource;

		public MasterViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Master", "Master");

			// Custom initialization
		}

		async void AddNewItem (object sender, EventArgs args)
		{

			SampleEntity entity = new SampleEntity ();
			entity.Id = DateTime.Now.Ticks;
			entity.SampleString = DateTime.Now.ToString ("s");

			try {
				await ServiceContainer.Resolve<SampleEntityDAO> ().SaveOrReplace (entity);
				dataSource.Objects.Insert (0, entity);
				using (var indexPath = NSIndexPath.FromRowSection (0, 0))
					TableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
			} catch (AWSErrorException e) {
				UIAlertView alert = new UIAlertView ("Error", e.ToString (), null, "OK", null);
				alert.Show ();
			}

		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			NavigationItem.LeftBarButtonItem = EditButtonItem;

			var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, AddNewItem);
			NavigationItem.RightBarButtonItem = addButton;

			TableView.Source = dataSource = new DataSource (this);

			RefreshControl = new UIRefreshControl ();
			RefreshControl.ValueChanged += async (object sender, EventArgs e) => {
				RefreshControl.BeginRefreshing ();
				await dataSource.RefreshData ();
				RefreshControl.EndRefreshing ();
				TableView.ReloadData ();
			};

		}

		class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString ("Cell");
			List<SampleEntity> objects = new List<SampleEntity> ();
			readonly MasterViewController controller;

			public DataSource (MasterViewController controller)
			{
				this.controller = controller;
				Initialize ();
			}

			private async void Initialize ()
			{
				try {
					await ServiceContainer.Resolve<SampleEntityDAO> ().CreateTable ();
				} catch (AWSErrorException e) {
					UIAlertView alert = new UIAlertView ("Error", e.ToString (), null, "OK", null);
					alert.Show ();
				}
			}

			public async Task RefreshData ()
			{
				try {
					this.Objects = await ServiceContainer.Resolve<SampleEntityDAO> ().GetAll (true);
				} catch (AWSErrorException e) {
					UIAlertView alert = new UIAlertView ("Error", e.ToString (), null, "OK", null);
					alert.Show ();
				}
			}

			public IList<SampleEntity> Objects {
				get { return objects; }
				set { objects = (List<SampleEntity>)value; }
			}

			// Customize the number of sections in the table view.
			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return objects.Count;
			}

			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = (UITableViewCell)tableView.DequeueReusableCell (CellIdentifier, indexPath);

				cell.TextLabel.Text = objects [indexPath.Row].SampleString;

				return cell;
			}

			public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}

			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					// Delete the row from the data source.
					try {
						ServiceContainer.Resolve<SampleEntityDAO> ().Delete (objects [indexPath.Row]);
						objects.RemoveAt (indexPath.Row);
						controller.TableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
					} catch (AWSErrorException e) {
						UIAlertView alert = new UIAlertView ("Error", e.ToString (), null, "OK", null);
						alert.Show ();
					}
				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
					// Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
				}
			}

			/*
			// Override to support rearranging the table view.
			public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
			{
			}
			*/

			/*
			// Override to support conditional rearranging of the table view.
			public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the item to be re-orderable.
				return true;
			}
			*/
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "showDetail") {
				var indexPath = TableView.IndexPathForSelectedRow;
				var item = dataSource.Objects [indexPath.Row];

				((DetailViewController)segue.DestinationViewController).SetDetailItem (item);
			}
		}
	}
}

