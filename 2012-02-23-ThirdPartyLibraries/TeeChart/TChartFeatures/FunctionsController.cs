using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using Steema.TeeChart;

namespace TChartFeatures
{
	public class FunctionsController : UITableViewController
	{
		private List<ItemInfo> _items;
		public TChart chart;
		public ChartViewController chartController;
				
		public List<ItemInfo> Items
		{
			get
			{
				if (_items == null)
					_items = ((FunctionsDataSource)TableView.DataSource).Items;
				
				return _items;
			}
		}
		
		public FunctionsController(TChart chart, ChartViewController chartController,UITableViewStyle style) : base(style)
		{
			this.chart=chart;
			this.chartController = chartController;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true/false if your app supports:
			// toInterfaceOrientation == UIInterfaceOrientation.LandscapeLeft;
			// toInterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
			// toInterfaceOrientation == UIInterfaceOrientation.Portrait;
			// toInterfaceOrientation == UIInterfaceOrientation.PortraitUpsideDown;
			return true;
		}
		
		public override void ViewDidLoad ()
		{			
			NavigationItem.Title = "Functions";
			
			TableView.DataSource = new FunctionsDataSource();
			TableView.Delegate = new FunctionsDelegate(this);
			
			UIBarButtonItem button= new UIBarButtonItem();
			button.Title = "Done";
			this.NavigationItem.SetRightBarButtonItem(button,true);			
	
            button.Clicked += delegate(object sender, EventArgs e) {			
				//this.NavigationController.PopToRootViewController(true);
				this.NavigationController.PopToViewController(chartController,true);
            };
			
			base.ViewDidLoad ();
		}
	}
}