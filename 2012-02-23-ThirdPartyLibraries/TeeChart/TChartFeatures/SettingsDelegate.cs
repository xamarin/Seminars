using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class SettingsDelegate : UITableViewDelegate
	{		
		private SettingsController _controller;
		private ChartViewController _chartController;
				
		public SettingsDelegate(SettingsController controller, ChartViewController chartController)
		{
			_controller = controller;	
			_chartController = chartController;
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewController nextController = null;
			
			switch (indexPath.Row)
			{
/*				case 0:
//					nextController = new SeriesTypesController(_controller.chart, _chartController ,UITableViewStyle.Grouped);
					nextController = new SeriesTypesController(UITableViewStyle.Grouped);
					break;*/
				case 0:
					nextController = new AspectController(_controller.chart,_chartController,UITableViewStyle.Grouped);
					break;
				case 1:
					nextController = new ThemesController(_controller.chart,_chartController,UITableViewStyle.Grouped);
					break;
				case 2:
					nextController = new ColorPalettesController(_controller.chart,_chartController,UITableViewStyle.Grouped);
					break;
				case 3:
					nextController = new LegendController(_controller.chart,_chartController,UITableViewStyle.Grouped);
					break;
				case 4:
					nextController = new ToolsController(_controller.chart,_chartController,UITableViewStyle.Grouped);
					break;
				case 5:
					nextController = new FunctionsController(_controller.chart,_chartController,UITableViewStyle.Grouped);
					break;
				case 6:
					nextController = new EditableTableController(UITableViewStyle.Grouped);
					break;
				default:
					break;
			}
					
			if (nextController != null)
				_controller.NavigationController.PushViewController(nextController,true);
		}
	}
}