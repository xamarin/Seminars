using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class SeriesTypesDelegate : UITableViewDelegate
	{		
		private SeriesTypesController _controller;
		//private NSIndexPath _previousRow;
		
		public SeriesTypesDelegate(SeriesTypesController controller,ChartViewController chartController)
		{
			_controller = controller;
			//_previousRow = NSIndexPath.FromRowSection(Settings.SelectedIndex,0);
		}
		
		public void _controller_GetAxisLabel(object sender, Steema.TeeChart.GetAxisLabelEventArgs e)
		{
			if (sender==_controller.chart.Axes.Left)
				((Steema.TeeChart.Axis)sender).Labels.Font.Color=UIColor.Red.CGColor;
		}
		
		private void line1_BeforeDrawValues(object sender, Steema.TeeChart.Drawing.Graphics3D g) 
		{
			int posAxis = 0;
			if(_controller.chart.Axes.Left.Maximum > 0)
				_controller.chart.Axes.Left.Draw(g.ChartXCenter - 10,g.ChartXCenter - 20,g.ChartXCenter,true);
			posAxis = _controller.chart.Axes.Left.CalcYPosValue(10);
			_controller.chart.Axes.Bottom.Draw(posAxis + 10, posAxis + 40, posAxis, true);
		}

		private void chart_AfterDraw(object sender, Steema.TeeChart.Drawing.Graphics3D g) 
		{
/*							_controller.chart.Canvas.TextOut(100,100,"hello");
			_controller.chart.Graphics3D.Pen.Color = UIColor.Yellow.CGColor;
			_controller.chart.Graphics3D.Pen.Width=3;
			_controller.chart.Canvas.Line(0,0,100,100);
	*/	}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{			
			// Uncheck the previous row
			//if (_previousRow != null)
			//	tableView.CellAt(_previousRow).Accessory = UITableViewCellAccessory.None;
			
			// Do something with the row
			var row = indexPath.Row;
			Settings.SelectedIndex = row;
			//tableView.CellAt(indexPath).Accessory = UITableViewCellAccessory.Checkmark;
			
			// Changes Series type
			_controller.chart.Series.Clear();
			
			// Set some chart options to improve speed
			_controller.chart.Clear();
	
			Steema.TeeChart.Themes.BlackIsBackTheme theme = new Steema.TeeChart.Themes.BlackIsBackTheme(_controller.chart.Chart);
			theme.Apply();
			Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(_controller.chart.Chart,Steema.TeeChart.Themes.Theme.OnBlackPalette);	
			
			_controller.chart.Axes.Bottom.Grid.Visible = false;
			_controller.chart.Axes.Left.Grid.DrawEvery = 3;
			_controller.chart.Axes.Left.MinorTicks.Visible = false;
			_controller.chart.Axes.Bottom.MinorTicks.Visible = false;
			_controller.chart.Header.Visible = false;			
			_controller.chart.Legend.Visible = false;
			_controller.chart.Aspect.View3D = true;			
			
			switch (row)
			{
				case 0:
				Steema.TeeChart.Styles.Line line1 = new Steema.TeeChart.Styles.Line();
				_controller.chart.Series.Add(line1);
				_controller.chart.Aspect.View3D = false;
				_controller.chart.Chart.Invalidate();
				/*Random Rnd = new Random();
				_controller.chart.Aspect.View3D = false;
				for(int t = 0; t <= 20; ++t)
					line1.Add(t, ((Rnd.Next(100)) + 1) - ((Rnd.Next(70)) + 1), UIColor.Yellow.CGColor);
				
				_controller.chart.Axes.Left.AxisPen.Color = UIColor.White.CGColor;
				_controller.chart.Axes.Bottom.AxisPen.Color = UIColor.White.CGColor;
				_controller.chart.Axes.Left.AxisPen.Width = 1;
				_controller.chart.Axes.Bottom.AxisPen.Width = 1;
				line1.BeforeDrawValues += new Steema.TeeChart.Styles.Series.PaintChartEventHandler(line1_BeforeDrawValues);
				*/
				    _controller.chart.Series[0].FillSampleValues(50);
			/*		_controller.chart.Header.Visible = true;
					_controller.chart.Header.Text = "My Header Text";
					_controller.chart.Header.Alignment = MonoTouch.CoreText.CTTextAlignment.Left;
					_controller.chart.Header.Font.Size=20;
					_controller.chart.Legend.Visible = true;
					_controller.chart.Legend.Gradient.Visible=false;
					_controller.chart.Legend.Transparency = 50;*/				
				/*
				_controller.chart.Legend.Alignment = Steema.TeeChart.LegendAlignments.Bottom;
				_controller.chart.Legend.Font.Color = UIColor.Black.CGColor;
				_controller.chart.Legend.Shadow.Visible=false;
					_controller.chart.Panel.MarginTop = 10;
				_controller.chart.SubHeader.Visible = true;
				_controller.chart.SubHeader.Alignment = MonoTouch.CoreText.CTTextAlignment.Right;
					_controller.chart.SubHeader.Text = "SubHeader";
				_controller.chart.SubHeader.Font.Color = UIColor.Yellow.CGColor;
				_controller.chart.SubHeader.Font.Size = 12;*/
					break;
				case 1:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Points());
				    _controller.chart.Series[0].FillSampleValues(100);
					break;
				case 2:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Area());	
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Area());	
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Area());	
					_controller.chart.Series[0].FillSampleValues(5);
					_controller.chart.Series[1].FillSampleValues(5);
					_controller.chart.Series[2].FillSampleValues(5);
					_controller.chart.Aspect.View3D = false;
				    Steema.TeeChart.Styles.Area area1 = _controller.chart.Series[0] as Steema.TeeChart.Styles.Area;
				    Steema.TeeChart.Styles.Area area2 = _controller.chart.Series[1] as Steema.TeeChart.Styles.Area;
				    Steema.TeeChart.Styles.Area area3 = _controller.chart.Series[2] as Steema.TeeChart.Styles.Area;
				    area1.Transparency = 40;
				    area2.Transparency = 40;
					area3.Transparency = 40;
	
					break;
				case 3:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.FastLine());
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Series[0].FillSampleValues(400);
					break;
				case 4:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.HorizLine());
					_controller.chart.Series[0].FillSampleValues(25);
					break;
				case 5:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Bar());
				  
					_controller.chart.Series[0].Add(3,"Pears",UIColor.Red.CGColor);
					_controller.chart.Series[0].Add(4,"Apples",UIColor.Blue.CGColor);
					_controller.chart.Series[0].Add(2,"Oranges",UIColor.Green.CGColor);
				(_controller.chart.Series[0] as Steema.TeeChart.Styles.Bar).Pen.Visible =false;
				(_controller.chart.Series[0] as Steema.TeeChart.Styles.Bar).Marks.Shadow.Visible=false;
								_controller.chart.AfterDraw += new Steema.TeeChart.PaintChartEventHandler(chart_AfterDraw);
				//_controller.chart.GetAxisLabel += new Steema.TeeChart.GetAxisLabelEventHandler(_controller_GetAxisLabel);
				_controller.chart.Header.Font.Name = "Arial";
				_controller.chart.Header.Font.Size = 20;
				
					/*_controller.chart.Series[0].FillSampleValues(5);
					_controller.chart.Series[0].FillSampleValues(5);
					_controller.chart.Series[0].FillSampleValues(5);
					_controller.chart.Series[0].FillSampleValues(5);
				    Steema.TeeChart.Styles.Bar bar1 = _controller.chart.Series[0] as Steema.TeeChart.Styles.Bar;
					bar1.ColorEachPoint = true;
					bar1.Marks.Visible = true;
				    bar1.Marks.Transparent = true;
					bar1.Marks.ArrowLength = -15;
				    bar1.Marks.Arrow.Visible = false;*/
								
					break;
				case 6:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.HorizBar());
					_controller.chart.Series[0].FillSampleValues(5);
					_controller.chart.Aspect.View3D=false;
				    Steema.TeeChart.Styles.HorizBar hbar1 = _controller.chart.Series[0] as Steema.TeeChart.Styles.HorizBar;				    
				    hbar1.MarksOnBar = true;
					break;
				case 7:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Pie());
					_controller.chart.Series[0].Add(30);
					_controller.chart.Series[0].Add(30);
					_controller.chart.Series[0].Add(40);
					_controller.chart.Series[0].Add(70);
				
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Pie).Circled = true;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Pie).EdgeStyle = Steema.TeeChart.Drawing.EdgeStyles.Flat;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Pie).BevelPercent=15;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Pie).Marks.Font.Color = UIColor.FromRGB(255,255,255).CGColor;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Pie).Marks.Font.Size = 10;			
					_controller.chart.Series[0].Marks.Visible=true;
					_controller.chart.Series[0].Marks.Transparent=true;				 
					_controller.chart.Legend.Visible = true;
					_controller.chart.Legend.Alignment = Steema.TeeChart.LegendAlignments.Bottom;
					_controller.chart.Legend.Transparent = true;
					_controller.chart.Legend.Font.Size = 10;
				   
					_controller.chart.Aspect.Chart3DPercent=40;
					break;
				case 8:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Shape());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Shape).Gradient.Visible =true;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Shape).Style = Steema.TeeChart.Styles.ShapeStyles.Circle;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Shape).Gradient.Visible = true;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Shape).Gradient.EndColor = UIColor.FromRGB(255,0,0).CGColor;
				
					_controller.chart.Series[0].FillSampleValues();
					break;
				case 9:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Arrow());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Arrow).ColorEachPoint=true;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 10:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Bubble());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Bubble).Pointer.Gradient.Visible = true;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Bubble).Pointer.Gradient.EndColor=UIColor.FromRGB(255,255,255).CGColor;	
					_controller.chart.Series[0].FillSampleValues();								
					break;
				case 11:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Gantt());
					_controller.chart.Legend.Visible = true;
					_controller.chart.Legend.Alignment = Steema.TeeChart.LegendAlignments.Bottom;
					_controller.chart.Legend.Transparent = true;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 12:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Candle());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 13:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Donut());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Donut).Circled = true;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Donut).Pen.Visible = false;
					_controller.chart.Series[0].Marks.Visible=false;
					_controller.chart.Legend.Visible = true;
				    _controller.chart.Legend.Transparent = true;
				    _controller.chart.Legend.Alignment = Steema.TeeChart.LegendAlignments.Top;
				    _controller.chart.Legend.Symbol.Pen.Visible = false;
				    _controller.chart.Legend.Font.Size = 12;				  
					_controller.chart.Series[0].FillSampleValues(4);				
					_controller.chart.Aspect.View3D=true;
					break;
				case 14:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Volume());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Volume).LinePen.Width = 2;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 15:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Bar3D());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 16:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Points3D());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 17:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Polar());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Polar).Circled = true;
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 18:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.PolarBar());
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 19:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Radar());
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 20:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Clock());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Clock).Circled = true;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 21:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.WindRose());
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 22:			
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Pyramid());
					_controller.chart.Series[0].FillSampleValues(4);	
					break;
				case 23:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Surface());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 24:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.LinePoint());
					_controller.chart.Aspect.View3D = false;
				    _controller.chart.Series[0].FillSampleValues();				
					break;
				case 25:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.BarJoin());
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 26:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.ColorGrid());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 27:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Waterfall());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 28:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Histogram());
					_controller.chart.Aspect.View3D = false;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Histogram).LinesPen.Visible=false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 29:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Error());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Error).ColorEachPoint=true;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Error).ErrorPen.Width = 5;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 30:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.ErrorBar());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.ErrorBar).ColorEachPoint=true;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 31:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Contour());
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 32:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Smith());
					_controller.chart.Aspect.View3D = false;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Smith).Circled=true;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 33:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Bezier());
					_controller.chart.Aspect.View3D=false;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Bezier).Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Bezier).Pointer.Pen.Visible=false;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Bezier).LinePen.Width = 2;				
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Bezier).LinePen.Color = UIColor.Red.CGColor;
					_controller.chart.Series[0].FillSampleValues(4);				
					break;
				case 34:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Calendar());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 35:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.HighLow());
					_controller.chart.Aspect.View3D=false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 36:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.TriSurface());
					_controller.chart.Aspect.View3D=true;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 37:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Funnel());
					_controller.chart.Aspect.View3D	= false;
					_controller.chart.Series[0].FillSampleValues(20);				
					break;
				case 38:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Box());
					_controller.chart.Series[0].FillSampleValues();		
					break;
				case 39:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.HorizBox());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 40:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.HorizArea());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 41:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Tower());
					_controller.chart.Aspect.View3D=true;
					_controller.chart.Series[0].FillSampleValues(5);		
					_controller.chart.Walls.Visible = false;				   
					_controller.chart.Axes.Bottom.Ticks.Visible = false;
					_controller.chart.Axes.Bottom.MinorTicks.Visible =false;	
					_controller.chart.Axes.Left.Ticks.Visible = false;
					_controller.chart.Axes.Left.MinorTicks.Visible =false;	
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.Tower).Pen.Visible=false;	
					_controller.chart.Tools.Add(new Steema.TeeChart.Tools.Rotate());
					_controller.chart.Aspect.Orthogonal = false;
					_controller.chart.Aspect.Rotation = -25;			
					_controller.chart.Aspect.Zoom = 70;	
					_controller.chart.Aspect.Chart3DPercent = 75;
				    _controller.chart.Header.Text = "Drag to Rotate the Chart";
				    _controller.chart.Header.Visible = true;
					break;
				case 42:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.PointFigure());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 43:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Gauges());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 44:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Vector3D());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 45:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.HorizHistogram());
					_controller.chart.Aspect.View3D = false;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.HorizHistogram).LinesPen.Visible=false;				
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 46:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Map());
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 47:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.ImageBar());
					_controller.chart.Series[0].FillSampleValues();		
				    UIImage img = UIImage.FromFile("bulb_on.png");
				    (_controller.chart.Series[0] as Steema.TeeChart.Styles.ImageBar).Image = img.CGImage;
					break;
				case 48:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Kagi());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 49:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Renko());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 50:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.IsoSurface());
					_controller.chart.Aspect.View3D=true;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 51:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Darvas());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 52:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.VolumePipe());
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Legend.Visible = true;
				    _controller.chart.Legend.Transparent = true;
				    _controller.chart.Legend.Alignment = Steema.TeeChart.LegendAlignments.Bottom;
				    _controller.chart.Legend.Symbol.Pen.Visible = false;
				    _controller.chart.Legend.Font.Size = 14;
					_controller.chart.Series[0].FillSampleValues(4);				
					break;
				case 53:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.ImagePoint());
					_controller.chart.Series[0].FillSampleValues(5);			
				    _controller.chart.Aspect.View3D	= false;
				    UIImage img2 = UIImage.FromFile("bulb_off.png");
				    (_controller.chart.Series[0] as Steema.TeeChart.Styles.ImagePoint).Pointer.HorizSize = 30;				
				    (_controller.chart.Series[0] as Steema.TeeChart.Styles.ImagePoint).Pointer.VertSize = 30;				
				    (_controller.chart.Series[0] as Steema.TeeChart.Styles.ImagePoint).PointImage = img2.CGImage;
					break;
				case 54:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.CircularGauge());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.CircularGauge).Value = 65;			
					break;
				case 55:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.LinearGauge());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 56:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.VerticalLinearGauge());		
				 	(_controller.chart.Series[0] as Steema.TeeChart.Styles.VerticalLinearGauge).Axis.Ticks.Visible = true;
				 	(_controller.chart.Series[0] as Steema.TeeChart.Styles.VerticalLinearGauge).Axis.MinorTicks.Visible = true;				
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 57:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.NumericGauge());
				    (_controller.chart.Series[0] as Steema.TeeChart.Styles.NumericGauge).Value = 123;
					break;
				case 58:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.OrgSeries());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 59:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.TagCloud());
					_controller.chart.Series[0].FillSampleValues(50);				
					break;
				case 60:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.PolarGrid());
					_controller.chart.Aspect.View3D = false;
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 61:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.Ternary());
					_controller.chart.Series[0].FillSampleValues();				
					break;
				case 62:
					_controller.chart.Series.Add(new Steema.TeeChart.Styles.KnobGauge());
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.KnobGauge).ActiveCenter=true;
					(_controller.chart.Series[0] as Steema.TeeChart.Styles.KnobGauge).Value = 50;
					break;
				default:
					break;				
			}
			/*
			_controller.chart.Series[0].Add(100);
			_controller.chart.Series[0].Add(150);
			_controller.chart.Series[0].Add(75);
			_controller.chart.Series[0].Add(30);
			*/
			//_controller.chart.Series[0].FillSampleValues(150);
			
			Console.WriteLine("{0} selected",_controller.Items[row]);
			//_previousRow = indexPath;
			
			// This is what the Settings does under Settings>Mail>Show on an iPhone
			tableView.DeselectRow(indexPath,false);
            _controller.NavigationController.PushViewController(_controller.chartController,true);			
		}
	}
}