using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;
using Steema.TeeChart;

namespace CustomDrawing
{
	public partial class CustomDrawingViewController : UIViewController
	{
		TChart Chart1;
		
		public CustomDrawingViewController (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//any additional setup after loading the view, typically from a nib.
			
			// Create the Chart
			Chart1 = new TChart();
			
			// Assign a Rect frame
			System.Drawing.RectangleF r = new System.Drawing.RectangleF(0,80,this.View.Bounds.Width,this.View.Bounds.Height-80);						
			Chart1.Frame = r;
			
			// Creates the AfterDraw Event call
			Chart1.AfterDraw += new Steema.TeeChart.PaintChartEventHandler(chart_AfterDraw);	
			
			// Adds the Chart view to the Root view
			this.View.AddSubview(Chart1);
			
	        //---- wire up our click me button which will make to call the afterdraw event
	        this.ButtonClick.TouchUpInside += (sender, e) => {
				   Chart1.Chart.Invalidate();
	        };			
		}

		private void chart_AfterDraw(object sender, Steema.TeeChart.Drawing.Graphics3D g) 
		{
			Random rnd= new Random();
			for (int i=0;i<2;i++)
			{
				Chart1.Canvas.Font.Size	= rnd.Next(8,30);
				Chart1.Canvas.Font.Color = UIColor.FromRGB(rnd.Next(255),rnd.Next(255),rnd.Next(255)).CGColor;
				Chart1.Canvas.TextOut(rnd.Next(300),rnd.Next(300),"TextOut!!! ");
				Chart1.Graphics3D.Pen.Color = UIColor.FromRGB(rnd.Next(255),rnd.Next(255),rnd.Next(255)).CGColor;
				Chart1.Graphics3D.Pen.Width=rnd.Next(1,5);				
				Chart1.Canvas.Line(rnd.Next(300),rnd.Next(300),rnd.Next(300),rnd.Next(300));
				Chart1.Canvas.Brush.Color = UIColor.Clear.CGColor;
				Chart1.Canvas.Ellipse(new Rectangle(rnd.Next(300),rnd.Next(300),rnd.Next(50,100),rnd.Next(50,100)));
				Chart1.Canvas.Brush.Color = UIColor.FromRGB(rnd.Next(255),rnd.Next(255),rnd.Next(255)).CGColor;
				Chart1.Canvas.Ellipse(new Rectangle(rnd.Next(300),rnd.Next(300),rnd.Next(50,100),rnd.Next(50,100)));
				Chart1.Canvas.Brush.Color = UIColor.FromRGB(rnd.Next(255),rnd.Next(255),rnd.Next(255)).CGColor;
				Chart1.Canvas.Rectangle(new Rectangle(rnd.Next(300),rnd.Next(300),rnd.Next(50,100),rnd.Next(50,100)));
			}
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Release any retained subviews of the main view.
			// e.g. myOutlet = null;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			// return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			return false;
		}
	}
}
