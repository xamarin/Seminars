using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class ColorPalettesDelegate : UITableViewDelegate
	{		
		private ColorPalettesController _controller;
		private NSIndexPath _previousRow;
		
		public ColorPalettesDelegate(ColorPalettesController controller)
		{
			_controller = controller;
			_previousRow = NSIndexPath.FromRowSection(Settings.SelectedIndex,0);
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			// Uncheck the previous row
		/*	if (_previousRow != null)
				tableView.CellAt(_previousRow).Accessory = UITableViewCellAccessory.None;
		*/	
			// Do something with the row
			var row = indexPath.Row;
			Settings.SelectedIndex = row;
			tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.Checkmark;
			
			// Applies the selected Color palette
				switch (row)
				{
					case 0:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.TeeChartPalette);		
						break;
					case 1:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.ExcelPalette);		
						break;
					case 2:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.VictorianPalette);		
						break;
					case 3:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.PastelsPalette);		
						break;
					case 4:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.SolidPalette);		
						break;
					case 5:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.ClassicPalette);		
						break;
					case 6:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.WebPalette);		
						break;
					case 7:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.ModernPalette);		
						break;
					case 8:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.RainbowPalette);		
						break;
					case 9:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.WindowsXPPalette);		
						break;
					case 10:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.MacOSPalette);		
						break;
					case 11:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.WindowsVistaPalette);		
						break;
					case 12:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.GrayscalePalette);		
						break;
					case 13:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.OperaPalette);		
						break;
					case 14:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.WarmPalette);		
						break;
					case 15:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.CoolPalette);		
						break;
					case 16:
						Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.OnBlackPalette);		
						break;
					default:
						break;				
				}
			_controller.chart.Chart.Invalidate();
			
			Console.WriteLine("{0} selected",_controller.Items[row]);
			
			_previousRow = indexPath;
			
			// This is what the Settings does under Settings>Mail>Show on an iPhone
			tableView.DeselectRow(indexPath,false);
			_controller.NavigationController.PopToViewController(_controller.chartController,true);
		}
	}
}