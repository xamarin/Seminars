using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using Steema.TeeChart;

namespace TChartFeatures
{
	public class SettingsController : UITableViewController
	{
		public TChart chart;
		public ChartViewController chartController;
		
		// Allow us to set the style of the TableView
		public SettingsController(TChart chart, ChartViewController chartController ,UITableViewStyle style) : base(style)
		{
			this.chart =  chart;
			this.chartController = chartController;
		}
		
		public override void ViewDidLoad ()
		{
			TableView.DataSource = new SettingsDataSource();
			TableView.Delegate = new SettingsDelegate(this, chartController);
			
			base.ViewDidLoad ();
		}
	}
}