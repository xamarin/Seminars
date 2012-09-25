using System;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;
using System.Drawing;

namespace Presentation.AnimationPresentation
{
	class PieChartLayer : CALayer
	{
		CGColor color = new CGColor (0xFF/255.0f, 0xD3/255.0f, 0x20/255.0f);
		CGColor otherColor = new CGColor (0x00/255.0f, 0x84/255.0f, 0xD1/255.0f);
		
		[Export ("angle")]
		public double Angle { get; set; }
		
		public PieChartLayer ()
		{
		}
		
		[Export ("initWithLayer:")]
		public PieChartLayer (CALayer other)
			: base (other)
		{
		}
		
		public override void Clone (CALayer other)
		{
			var o = (PieChartLayer)other;
			Angle = o.Angle;
			base.Clone (other);
		}
		
		[Export ("needsDisplayForKey:")]
		static bool NeedsDisplayForKey (NSString key)
		{
			return key.ToString () == "angle" ||
				CALayer.NeedsDisplayForKey (key);
		}
		
		public override void DrawInContext (CGContext ctx)
		{
			base.DrawInContext (ctx);
			
			var bounds = Bounds;
			var c = new PointF (bounds.GetMidX (), bounds.GetMidY ());
			
			ctx.BeginPath ();
			ctx.MoveTo (c.X, c.Y);
			ctx.AddLineToPoint (bounds.Right, c.Y);
			ctx.AddArc (c.X, c.Y, bounds.Width/2, (float)0, (float)Angle, false);
			ctx.AddLineToPoint (c.X, c.Y);
			ctx.SetFillColor (otherColor);
			ctx.FillPath ();
			
			ctx.BeginPath ();
			ctx.MoveTo (c.X, c.Y);
			ctx.AddLineToPoint (bounds.Right, c.Y);
			ctx.AddArc (c.X, c.Y, bounds.Width/2, (float)0, (float)(1e-7 + Angle), true);
			ctx.AddLineToPoint (c.X, c.Y);
			ctx.SetFillColor (color);
			ctx.FillPath ();
			
		}
	}
}

