using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class ThemesDelegate : UITableViewDelegate
	{		
		private ThemesController _controller;
		private NSIndexPath _previousRow;
		
		public ThemesDelegate(ThemesController controller)
		{
			_controller = controller;
			_previousRow = NSIndexPath.FromRowSection(Settings.SelectedIndex,0);
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			// Uncheck the previous row
			/*if (_previousRow != null)
				tableView.CellAt(_previousRow).Accessory = UITableViewCellAccessory.None;
			*/
			// Do something with the row
			var row = indexPath.Row;
			Settings.SelectedIndex = row;
			tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.Checkmark;
			
			// Applies the selected theme
				switch (row)
				{
					case 0:
						Steema.TeeChart.Themes.BlackIsBackTheme theme = new Steema.TeeChart.Themes.BlackIsBackTheme(_controller.chart.Chart);
						theme.Apply();
						break;
					case 1:
						Steema.TeeChart.Themes.OperaTheme theme1 = new Steema.TeeChart.Themes.OperaTheme(_controller.chart.Chart);
						theme1.Apply();
						break;
					case 2:
						Steema.TeeChart.Themes.TeeChartTheme theme2 = new Steema.TeeChart.Themes.TeeChartTheme(_controller.chart.Chart);
						theme2.Apply();
						break;
					case 3:
						Steema.TeeChart.Themes.ExcelTheme theme3 = new Steema.TeeChart.Themes.ExcelTheme(_controller.chart.Chart);
						theme3.Apply();
						break;
					case 4:
						Steema.TeeChart.Themes.ClassicTheme theme4 = new Steema.TeeChart.Themes.ClassicTheme(_controller.chart.Chart);
						theme4.Apply();
						break;
					case 5:
						Steema.TeeChart.Themes.XPTheme theme5 = new Steema.TeeChart.Themes.XPTheme(_controller.chart.Chart);
						theme5.Apply();
						break;
					case 6:
						Steema.TeeChart.Themes.WebTheme theme6 = new Steema.TeeChart.Themes.WebTheme(_controller.chart.Chart);
						theme6.Apply();
						break;
					case 7:
						Steema.TeeChart.Themes.BusinessTheme theme7 = new Steema.TeeChart.Themes.BusinessTheme(_controller.chart.Chart);
						theme7.Apply();
						break;
					case 8:
						Steema.TeeChart.Themes.BlueSkyTheme theme8 = new Steema.TeeChart.Themes.BlueSkyTheme(_controller.chart.Chart);
						theme8.Apply();
						break;
					case 9:
						Steema.TeeChart.Themes.GrayscaleTheme theme9 = new Steema.TeeChart.Themes.GrayscaleTheme(_controller.chart.Chart);
						theme9.Apply();
						break;
					case 10:
						Steema.TeeChart.Themes.RandomTheme theme10 = new Steema.TeeChart.Themes.RandomTheme(_controller.chart.Chart);
						theme10.Apply();
						break;
					default:
						break;				
				}
			
			Console.WriteLine("{0} selected",_controller.Items[row]);
			
			_previousRow = indexPath;
			
			// This is what the Settings does under Settings>Mail>Show on an iPhone
			tableView.DeselectRow(indexPath,false);
			_controller.NavigationController.PopToViewController(_controller.chartController,true);
		}
	}
}
