using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;

namespace Presentation.AnimationPresentation
{
	[Demo ("Custom Properties", "Explicit Animations")]
	public class ExplicitCustom : DemoViewController
	{
		PieChartLayer pieChart;
		
		public ExplicitCustom ()
		{
			pieChart = new PieChartLayer ();
			View.Layer.AddSublayer (pieChart);
			Reset ();
		}
		
		public void Reset ()
		{
			pieChart.Frame = new RectangleF (100, 100, 200, 230);
			pieChart.Angle = Math.PI / 4;
			pieChart.SetNeedsDisplay ();
		}

		public void Basic ()
		{
			var a = new CABasicAnimation {
				From = NSNumber.FromDouble (pieChart.Angle),
				To = NSNumber.FromDouble (3*Math.PI/2),
				KeyPath = "angle",
				Duration = 3,
				TimingFunction = CAMediaTimingFunction.FromName (
					CAMediaTimingFunction.EaseInEaseOut),
			};
			pieChart.AddAnimation (a, "AngleAnimation");
			pieChart.Angle = 3*Math.PI/2;
		}
	}
}

