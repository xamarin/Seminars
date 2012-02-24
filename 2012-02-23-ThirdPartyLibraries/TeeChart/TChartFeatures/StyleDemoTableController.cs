using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Drawing;

namespace TChartFeatures
{
	public class StyleDemoTableController : UITableViewController
	{
		public StyleDemoTableController(UITableViewStyle style) : base(style)
		{	
		}
		
		public override void ViewDidLoad ()
		{
			NavigationItem.Title = "Styled table demo";
			
			TableView.DataSource = new StyleDemoTableDataSource(this);
			TableView.Delegate = new StyleDemoTableDelegate(this);
			
			base.ViewDidLoad ();
		}
	}
}
