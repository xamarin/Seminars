using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;

namespace Presentation.AnimationPresentation
{
	[Demo ("Keyframe", "Explicit Animations")]
	public class ExplicitKeyframe : DemoViewController
	{
		UIView solidView;
		
		public ExplicitKeyframe ()
		{
			solidView = new UIView {
				BackgroundColor = UIColor.White,
			};
			View.AddSubview (solidView);
			Reset ();
		}
		
		public void Reset ()
		{
			solidView.Layer.Transform = CATransform3D.Identity;
			solidView.Layer.Frame = new RectangleF (100, 100, 200, 230);
			solidView.Layer.Opacity = 1.0f;
			solidView.Layer.CornerRadius = 5;
			solidView.Layer.ShadowOffset = new SizeF (0, 20);
			solidView.Layer.ShadowRadius = 10;
			solidView.Layer.ShadowColor = new CGColor (0, 0, 0);
			solidView.Layer.ShadowOpacity = 1.0f;
		}

		public void Position ()
		{
			var startPosition = solidView.Layer.Position;
			
			var a = new CAKeyFrameAnimation {
				KeyTimes = new [] {
					NSNumber.FromDouble (0),
					NSNumber.FromDouble (0.25),
					NSNumber.FromDouble (0.5),
					NSNumber.FromDouble (0.625),
					NSNumber.FromDouble (0.75),
					NSNumber.FromDouble (0.75+0.125/2),
					NSNumber.FromDouble (0.75+0.125),
					NSNumber.FromDouble (1),
				},
				Values = new NSObject[] {
					NSValue.FromPointF (startPosition),
					NSValue.FromPointF (new PointF (startPosition.X, startPosition.Y-100)),
					NSValue.FromPointF (startPosition),
					NSValue.FromPointF (new PointF (startPosition.X, startPosition.Y-25)),
					NSValue.FromPointF (startPosition),
					NSValue.FromPointF (new PointF (startPosition.X, startPosition.Y-12.5f)),
					NSValue.FromPointF (startPosition),
				},
				TimingFunction = CAMediaTimingFunction.FromName (
					CAMediaTimingFunction.EaseInEaseOut),
				Duration = 4,
				KeyPath = "position",
			};
			solidView.Layer.AddAnimation (a, "PositionAnimation");
		}

		public void CornerRadius ()
		{
			var a = new CAKeyFrameAnimation {
				KeyTimes = new [] {
					NSNumber.FromDouble (0),
					NSNumber.FromDouble (0.1),
					NSNumber.FromDouble (0.5),
					NSNumber.FromDouble (0.9),
					NSNumber.FromDouble (1),
				},
				Values = new NSObject[] {
					NSNumber.FromFloat (5),
					NSNumber.FromFloat (50),
					NSNumber.FromFloat (25),
					NSNumber.FromFloat (50),
					NSNumber.FromFloat (5),
				},
				Duration = 2,
				KeyPath = "cornerRadius",
			};
			solidView.Layer.AddAnimation (a, "CornerRadiusAnimation");
		}
	}
}

