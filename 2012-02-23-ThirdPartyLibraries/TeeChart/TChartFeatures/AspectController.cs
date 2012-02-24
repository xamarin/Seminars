using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Drawing;
using Steema.TeeChart;

namespace TChartFeatures
{
	public class AspectController : UITableViewController
	{
		public TChart chart;
		public ChartViewController chartController;
		
		public AspectController(TChart chart,ChartViewController chartController, UITableViewStyle style) : base(style)
		{	
			this.chart=chart;
			this.chartController = chartController;
		}
		
		public override void ViewDidLoad ()
		{
			NavigationItem.Title = "Aspect";
			
			TableView.DataSource = new AspectDataSource(this);
			TableView.Delegate = new AspectDelegate(this);

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
