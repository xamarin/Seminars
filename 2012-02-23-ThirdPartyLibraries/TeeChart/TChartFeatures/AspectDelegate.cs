using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Drawing;

namespace TChartFeatures
{
	public class AspectDelegate : UITableViewDelegate
	{		
		private AspectController _controller;
		
		public AspectDelegate(AspectController controller)
		{
			_controller = controller;	
		}
		
		public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
		{
			// This will only fire for item 5.
			Console.WriteLine("Row {0} clicked",indexPath.Row);
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
/* pep commented temp			UITableViewController nextController = new CheckmarkDemoTableController(UITableViewStyle.Grouped);
			_controller.NavigationController.PushViewController(nextController,true);*/
			
		}
	}
}