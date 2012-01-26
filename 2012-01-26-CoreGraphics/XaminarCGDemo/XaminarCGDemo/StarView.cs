using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace XaminarCGDemo
{
    public class StarView : UIView
    {
        public StarView ()
        {
        }
        
        public override void Draw (RectangleF rect)
        {
            base.Draw (rect);
            
            // get graphics context
            CGContext gctx = UIGraphics.GetCurrentContext ();
            
            // set up drawing attributes
            gctx.SetLineWidth (4);
            UIColor.Yellow.SetStroke ();
            
            // stroke with a dashed line
            gctx.SetLineDash (3, new float[] {6,2});     
            
            // create geometry
            var path = new CGPath ();
            
            PointF origin = new PointF (Bounds.GetMidX (), 
                                        Bounds.GetMinY () + 10);
            
            path.AddLines (new PointF[] { 
                origin, 
                new PointF (origin.X + 35, origin.Y + 80), 
                new PointF (origin.X - 50, origin.Y + 30), 
                new PointF (origin.X + 50, origin.Y + 30), 
                new PointF (origin.X - 35, origin.Y + 80) });
            
            path.CloseSubpath ();
            
            // add geometry to graphics context and draw it
            gctx.AddPath (path);
         
            gctx.DrawPath (CGPathDrawingMode.Stroke);

            // fill the star with a gradient          
            gctx.AddPath (path);          
            gctx.Clip ();
                 
            RectangleF starBoundingBox = path.BoundingBox;
            
            float[] locations = { 0.0f, 1.0f };
            float[] components = { 1.0f, 0.0f, 0.0f, 1.0f,
                                   0.0f, 0.0f, 1.0f, 1.0f };
            
            using (var rgb = CGColorSpace.CreateDeviceRGB()) {
                CGGradient gradient = new CGGradient (rgb, components, locations);  
 
                PointF gradientStart = new PointF (starBoundingBox.Left, starBoundingBox.Top);
                PointF gradientEnd = new PointF (starBoundingBox.Right, starBoundingBox.Bottom);
                gctx.DrawLinearGradient (gradient, gradientStart, gradientEnd, CGGradientDrawingOptions.DrawsBeforeStartLocation);
            }
        }
    }
}



