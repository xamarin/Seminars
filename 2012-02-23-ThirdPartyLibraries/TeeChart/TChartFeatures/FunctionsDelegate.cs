using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class FunctionsDelegate : UITableViewDelegate
	{		
		private FunctionsController _controller;
		private NSIndexPath _previousRow;
		
		public FunctionsDelegate(FunctionsController controller)
		{
			_controller = controller;
			_previousRow = NSIndexPath.FromRowSection(Settings.SelectedIndex,0);
		}
		
		private Steema.TeeChart.Styles.Line line1;
			
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
			
			line1 = new Steema.TeeChart.Styles.Line();
			_controller.chart.Chart.Series.Add(line1);
			line1.DataSource = _controller.chart.Chart.Series[0];
							
			// Applies the selected Function
				switch (row)
				{
					case 0:				        
						line1.Function = new Steema.TeeChart.Functions.Add();break;
					case 1:
						line1.Function = new Steema.TeeChart.Functions.Subtract();break;
					case 2:
						line1.Function = new Steema.TeeChart.Functions.Multiply();break;
					case 3:
						line1.Function = new Steema.TeeChart.Functions.Divide();break;
					case 4:
						line1.Function = new Steema.TeeChart.Functions.High();break;
					case 5:
						line1.Function = new Steema.TeeChart.Functions.Low();break;
					case 6:
						line1.Function = new Steema.TeeChart.Functions.Average();break;
					case 7:
						line1.Function = new Steema.TeeChart.Functions.Count();break;
					case 8:
						line1.Function = new Steema.TeeChart.Functions.Momentum();break;
					case 9:
						line1.Function = new Steema.TeeChart.Functions.MomentumDivision();break;
					case 10:
						line1.Function = new Steema.TeeChart.Functions.Cumulative();break;
					case 11:
						line1.Function = new Steema.TeeChart.Functions.ExpAverage();break;
					case 12:
						line1.Function = new Steema.TeeChart.Functions.Smoothing();break;
					case 13:
						line1.Function = new Steema.TeeChart.Functions.Custom();break;
					case 14:
						line1.Function = new Steema.TeeChart.Functions.RootMeanSquare();break;
					case 15:
						line1.Function = new Steema.TeeChart.Functions.StdDeviation();break;
					case 16:
						line1.Function = new Steema.TeeChart.Functions.Stochastic();break;
					case 17:
						line1.Function = new Steema.TeeChart.Functions.ExpMovAverage();break;
					case 18:
						line1.Function = new Steema.TeeChart.Functions.Performance();break;
					case 19:
						line1.Function = new Steema.TeeChart.Functions.CrossPoints();break;
					case 20:
						line1.Function = new Steema.TeeChart.Functions.CompressOHLC();break;
					case 21:
						line1.Function = new Steema.TeeChart.Functions.CLVFunction();break;
					case 22:
						line1.Function = new Steema.TeeChart.Functions.OBVFunction();break;
					case 23:
						line1.Function = new Steema.TeeChart.Functions.CCIFunction();break;
					case 24:
						line1.Function = new Steema.TeeChart.Functions.MovingAverage();break;
					case 25:
						line1.Function = new Steema.TeeChart.Functions.PVOFunction();break;
					case 26:
						line1.Function = new Steema.TeeChart.Functions.DownSampling();break;
					case 27:
						line1.Function = new Steema.TeeChart.Functions.TrendFunction();break;
					case 28:
						line1.Function = new Steema.TeeChart.Functions.CorrelationFunction();break;
					case 29:
						line1.Function = new Steema.TeeChart.Functions.VarianceFunction();break;
					case 30:
						line1.Function = new Steema.TeeChart.Functions.PerimeterFunction();break;
					case 31:
						line1.Function = new Steema.TeeChart.Functions.PolyFitting();break;
					case 32:
						line1.Function = new Steema.TeeChart.Functions.Bollinger();break;
					case 33:
						line1.Function = new Steema.TeeChart.Functions.MACDFunction();break;
					case 34:
						line1.Function = new Steema.TeeChart.Functions.RSIFunction();break;
					case 35:
						line1.Function = new Steema.TeeChart.Functions.ADXFunction();break;
					case 36:
						line1.Function = new Steema.TeeChart.Functions.MedianFunction();break;
					case 37:
						line1.Function = new Steema.TeeChart.Functions.ModeFunction();break;
					case 38:
						line1.Function = new Steema.TeeChart.Functions.ExpTrendFunction();break;
					case 39:
						line1.Function = new Steema.TeeChart.Functions.HistogramFunction();break;
					case 40:
						line1.Function = new Steema.TeeChart.Functions.SARFunction();break;
					default:
						break;				
				}
			_controller.chart.Chart.Invalidate();
			
			//Console.WriteLine("{0} selected",_controller.Items[row]);
			
			_previousRow = indexPath;
			
			// This is what the Settings does under Settings>Mail>Show on an iPhone
			tableView.DeselectRow(indexPath,false);
			_controller.NavigationController.PopToViewController(_controller.chartController,true);
		}
	}
}