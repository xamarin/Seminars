using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using Steema.TeeChart;

namespace TChartFeatures
{
	public class SeriesTypesController : UITableViewController
	{
		private List<ItemInfo> _items;
		public TChart chart;
		public ChartViewController chartController;
				
		public List<ItemInfo> Items
		{
			get
			{
				if (_items == null)
					_items = ((SeriesTypesDataSource)TableView.DataSource).Items;
				
				return _items;
			}
		}
		
//		public SeriesTypesController(TChart chart, ChartViewController chartController,UITableViewStyle style) : base(style)
		public SeriesTypesController(UITableViewStyle style) : base(style)
		{
	//		this.chart=chart;
	//		this.chartController = chartController;
			
				this.chartController =new ChartViewController();
				this.chart = chartController.chart;
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
			NavigationItem.Title = "Series Styles";

			
			TableView.DataSource = new SeriesTypesDataSource();
			TableView.Delegate = new SeriesTypesDelegate(this,this.chartController);
			
			/*UIBarButtonItem button= new UIBarButtonItem();
			button.Title = "Done";
			this.NavigationItem.SetRightBarButtonItem(button,true);
	
            button.Clicked += delegate(object sender, EventArgs e) {			
			//	this.NavigationController.PopToRootViewController(true);
				//this.NavigationController.PopToViewController(chartController,true);
				//this.NavigationController.PopViewControllerAnimated(true);
				
                NavigationController.PushViewController(chartController,true);
            };*/
			
			base.ViewDidLoad ();
		}
	}
}