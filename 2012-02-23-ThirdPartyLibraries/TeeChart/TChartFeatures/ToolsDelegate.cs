using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class ToolsDelegate : UITableViewDelegate
	{		
		private ToolsController _controller;
		
		public ToolsDelegate(ToolsController controller)
		{
			_controller = controller;
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			// Do something with the row
			var row = indexPath.Row;
			Settings.SelectedIndex = row;
			tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.Checkmark;
				
				
			// Applies the selected Tool
			switch (row)
			{
				case 0:
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.Annotation());
				    int i = _controller.chart.Tools.Count-1;
				    (_controller.chart.Tools[i] as Steema.TeeChart.Tools.Annotation).Text = "My Annotation";
				    (_controller.chart.Tools[i] as Steema.TeeChart.Tools.Annotation).Top = 50;
				    (_controller.chart.Tools[i] as Steema.TeeChart.Tools.Annotation).Shape.Left = 100;
				    (_controller.chart.Tools[i] as Steema.TeeChart.Tools.Annotation).Shape.Font.Size = 15;		
					break;
				case 1:
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.ChartImage());
				    (_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ChartImage).Image = UIImage.FromFile("SeriesIcons/line.png").CGImage;
					break;
				case 2:
				    _controller.chart.Chart.Series.RemoveAllSeries();
					_controller.chart.Chart.Aspect.ClipPoints=false;

					_controller.chart.Chart.Series.Add(new Steema.TeeChart.Styles.Line());
					_controller.chart.Chart.Series.Add(new Steema.TeeChart.Styles.Line());
					_controller.chart.Chart.Series[0].FillSampleValues(4);
					_controller.chart.Chart.Series[1].FillSampleValues(4);				
					_controller.chart.Chart.Legend.LegendStyle = Steema.TeeChart.LegendStyles.Values;
					_controller.chart.Chart.Legend.Visible=true;
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.ExtraLegend());
					_controller.chart.Legend.Visible = true;				    
				    (_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ExtraLegend).Series = _controller.chart.Series[1];
				    (_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ExtraLegend).Legend.LegendStyle = Steema.TeeChart.LegendStyles.Values;
				    (_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ExtraLegend).Legend.Left = 100;
				    (_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ExtraLegend).Legend.Top = 100;
					break;
				case 3:
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.GridBand());
				    (_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.GridBand).Axis = _controller.chart.Axes.Left;
				    (_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.GridBand).Band1.Color = UIColor.Gray.CGColor;
				    (_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.GridBand).Band2.Color = UIColor.DarkGray.CGColor;				
					break;
				case 4:
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.GridTranspose());
				    // should be a surface serues
					break;
				case 5:
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.Rotate());
					break;
				case 6:
				    _controller.chart.Chart.Series.RemoveAllSeries();
					_controller.chart.Chart.Aspect.ClipPoints=true;
				    _controller.chart.Chart.Aspect.ZoomScrollStyle=Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Manual;
					_controller.chart.Chart.Series.Add(new Steema.TeeChart.Styles.Bar());
					_controller.chart.Chart.Series[0].Add(10);
					_controller.chart.Chart.Series[0].Add(20);
					_controller.chart.Chart.Series[0].Add(50);
					_controller.chart.Chart.Series[0].Add(14);
					_controller.chart.Chart.Series[0].Add(5);
					_controller.chart.Chart.Series[0].Add(40);
								
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.ColorLine());
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ColorLine).Axis = _controller.chart.Axes.Left;
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ColorLine).Value = 30;
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ColorLine).Pen.Width = 2;					
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ColorLine).Pen.Color = UIColor.Red.CGColor;
					break;
				case 7:			
				    _controller.chart.Chart.Series.RemoveAllSeries();
					_controller.chart.Chart.Aspect.ClipPoints=true;
				    _controller.chart.Chart.Aspect.ZoomScrollStyle=Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Manual;
					_controller.chart.Chart.Series.Add(new Steema.TeeChart.Styles.Line());
					_controller.chart.Chart.Series[0].Add(10);
					_controller.chart.Chart.Series[0].Add(20);
					_controller.chart.Chart.Series[0].Add(50);
					_controller.chart.Chart.Series[0].Add(14);
					_controller.chart.Chart.Series[0].Add(5);
					_controller.chart.Chart.Series[0].Add(40);
							
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.ColorBand());
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ColorBand).Axis = _controller.chart.Axes.Left;
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ColorBand).Start = 20;
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ColorBand).End = 36;
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ColorBand).Pen.Width = 2;					
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.ColorBand).Pen.Color = UIColor.Red.CGColor;
					break;
				case 8:
				    _controller.chart.Chart.Aspect.ZoomScrollStyle=Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Manual;				
				    Steema.TeeChart.TChart c = _controller.chart;
				    c.Chart.Series.RemoveAllSeries();
					c.Chart.Series.Add(new Steema.TeeChart.Styles.Line());
					c.Chart.Series.Add(new Steema.TeeChart.Styles.Line());
					c.Chart.Series[0].FillSampleValues();
					c.Chart.Series[1].FillSampleValues();
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.SeriesBandTool());
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SeriesBandTool).Series = c.Chart.Series[0];
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SeriesBandTool).Series2 = c.Chart.Series[1];
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SeriesBandTool).Gradient.Visible = true;
					break;
				case 9:
				    _controller.chart.Chart.Aspect.ZoomScrollStyle=Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Manual;				
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.SubChartTool());
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SubChartTool).Charts.AddChart("My SubChart");
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SubChartTool).Charts[0].Chart.Series.Add(new Steema.TeeChart.Styles.Area());
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SubChartTool).Charts[0].Chart.Series[0].FillSampleValues(4);
					break;
				case 10:
				    Steema.TeeChart.TChart cc = _controller.chart;
				    cc.Chart.Series.RemoveAllSeries();
					cc.Chart.Series.Add(new Steema.TeeChart.Styles.Line());		
					cc.Chart.Series[0].Add(100);
					cc.Chart.Series[0].Add(300);
					cc.Chart.Series[0].Add(600);
					cc.Chart.Series[0].Add(800);
					cc.Chart.Series[0].Add(700);
					cc.Chart.Series[0].Add(200);
					cc.Chart.Aspect.View3D=false;
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.SeriesRegionTool());
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SeriesRegionTool).Series=cc.Chart.Series[0];				
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SeriesRegionTool).UseOrigin = true;
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SeriesRegionTool).Origin=500;
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SeriesRegionTool).Color = UIColor.Yellow.CGColor;
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.SeriesRegionTool).Transparency=50;
					break;
				case 11:
				    Steema.TeeChart.TChart c2 = _controller.chart;
				    c2.Chart.Series.RemoveAllSeries();
					c2.Chart.Series.Add(new Steema.TeeChart.Styles.Surface());
					c2.Chart.Series[0].FillSampleValues();
					c2.Chart.Legend.Visible=true;
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.LegendPalette());
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.LegendPalette).Series=c2.Chart.Series[0];				
					break;
				case 12:
				    Steema.TeeChart.TChart c3 = _controller.chart;
				    c3.Chart.Series.RemoveAllSeries();
					c3.Chart.Series.Add(new Steema.TeeChart.Styles.Line());
					c3.Chart.Series[0].FillSampleValues(100);
					c3.Chart.Aspect.ZoomScrollStyle = Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Manual;
					c3.Chart.Aspect.View3D=false;
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.AxisBreaksTool());
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.AxisBreaksTool).Axis=c3.Axes.Bottom;
					(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.AxisBreaksTool).GapSize=20;
				    Steema.TeeChart.Tools.AxisBreak break1 = new Steema.TeeChart.Tools.AxisBreak((_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.AxisBreaksTool));
				    Steema.TeeChart.Tools.AxisBreak break2 = new Steema.TeeChart.Tools.AxisBreak((_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.AxisBreaksTool));
				    break1.StartValue = 20;
				    break1.EndValue = 30;
					break2.StartValue = 50;
					break2.EndValue = 60;
					break;
				case 13:
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.DataTableTool());
					//(_controller.chart.Tools[_controller.chart.Tools.Count-1] as Steema.TeeChart.Tools.DataTableTool).TableLegend.Visible=true;
					break;				
				case 14:
					break;
				case 15:
					break;
				case 16:
					break;
				case 17:
					break;
				case 18:
					break;
				case 19:
					break;
				case 20:
					break;
				case 21:
					break;
				case 22:
					break;
				case 23:
					break;
				case 24:
					break;
				case 25:
					break;
				case 26:
					break;
				case 27:
					break;
				case 28:
					break;
				case 29:
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.MarksTip());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.NearestPoint());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.PageNumber());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.PieTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.LegendScrollBar());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.SeriesAnimation());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.SurfaceNearestTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.CursorTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.DragMarks());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.AxisArrow());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.DrawLine());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.DragPoint());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.GanttTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.AxisScroll());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.SeriesHotspot());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.ZoomTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.ScrollTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.LightTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.FibonacciTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.Marker());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.FaderTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.RectangleTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.Selector());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.SeriesStats());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.SeriesTranspose());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.ClipSeries());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.BannerTool());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.Magnify());
					//_controller.chart.Tools.Add(new Steema.TeeChart.Tools.CustomHotspot());
					break;
				case 30:
					break;
				case 31:
					break;
				case 32:
					break;
				case 33:
					break;
				case 34:
					break;
				case 35:
					break;
				case 36:
					break;
				case 37:
					break;
				case 38:
					break;
				case 39:
					break;
				case 40:
					break;
				case 41:
					break;
				case 42:
					break;
				default:
					break;				
			}
			
			_controller.chart.Chart.Invalidate();
			
			Console.WriteLine("{0} selected",_controller.Items[row]);
			
			// This is what the Settings does under Settings>Mail>Show on an iPhone
			tableView.DeselectRow(indexPath,false);
			_controller.NavigationController.PopToViewController(_controller.chartController,true);
		}
	}
}
