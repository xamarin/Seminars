using System;
using MonoTouch.UIKit;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Presenter
{
	public class DemosController : UITableViewController
	{
		List<Tuple<string, List<Demo>>> demos;

		public DemosController (Assembly asm)
			: base (UITableViewStyle.Grouped)
		{
			Title = "Xamarin Demos";

			var vcType = typeof (DemoViewController);

			var q = from t in asm.GetTypes ()
					where vcType.IsAssignableFrom (t) && !t.IsAbstract && t.IsPublic
					let d = new Demo (t)
					group d by d.Group;
			demos = q.Select (x => Tuple.Create (x.Key, x.ToList ())).ToList ();

			TableView.Delegate = new DemosDelegate (this);
			TableView.DataSource = new DemosDataSource (this);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		void PresentDemo (Demo demo)
		{
			var vc = (UIViewController)Activator.CreateInstance (demo.ViewControllerType);
			AppDelegate.Shared.SetDetailViewController (vc);
			NavigationController.PushViewController (new CommandsController (demo, vc), true);
		}

		class DemosDelegate : UITableViewDelegate
		{
			DemosController controller;
			public DemosDelegate (DemosController controller)
			{
				this.controller = controller;
			}

			public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var demos = controller.demos[indexPath.Section];
				controller.PresentDemo (demos.Item2[indexPath.Row]);
			}
		}

		class DemosDataSource : UITableViewDataSource
		{
			DemosController controller;
			public DemosDataSource (DemosController controller)
			{
				this.controller = controller;
			}

			public override int NumberOfSections (UITableView tableView)
			{
				return controller.demos.Count;
			}

			public override int RowsInSection (UITableView tableView, int section)
			{
				var demos = controller.demos[section];
				return demos.Item2.Count;
			}

			public override string TitleForHeader (UITableView tableView, int section)
			{
				var demos = controller.demos[section];
				return demos.Item1;
			}

			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell ("D");
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, "D");
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}

				var demos = controller.demos[indexPath.Section];
				var demo = demos.Item2[indexPath.Row];
				cell.TextLabel.Text = demo.Name;

				return cell;
			}
		}
	}
}

