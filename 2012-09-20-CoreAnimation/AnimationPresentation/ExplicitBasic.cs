using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;

namespace Presentation.AnimationPresentation
{
	[Demo ("Basic", "Explicit Animations")]
	public class ExplicitBasic : DemoViewController
	{
		UIView solidView;
		
		public ExplicitBasic ()
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

		public void To ()
		{
			var a = new CABasicAnimation {
				To = NSNumber.FromFloat (50),
				KeyPath = "cornerRadius",
				Duration = 2,
			};
			solidView.Layer.AddAnimation (a, "CornerRadiusAnimation");
		}
		
		public void FromTo ()
		{
			var a = new CABasicAnimation {
				From = NSNumber.FromFloat (50),
				To = NSNumber.FromFloat (5),
				KeyPath = "cornerRadius",
				Duration = 2,
			};
			solidView.Layer.AddAnimation (a, "CornerRadiusAnimation");
		}
		
		public void ToDidStop ()
		{
			var a = new CABasicAnimation {
				To = NSNumber.FromFloat (50),
				KeyPath = "cornerRadius",
				Duration = 2,
			};
			a.AnimationStopped += (sender, e) => {
				solidView.Layer.CornerRadius = 50;
			}; 
			solidView.Layer.AddAnimation (a, "CornerRadiusAnimation");
		}

		public void By ()
		{
			var a = new CABasicAnimation {
				By = NSNumber.FromFloat (45),
				KeyPath = "cornerRadius",
				Duration = 2,
			};
			solidView.Layer.AddAnimation (a, "CornerRadiusAnimation");
		}
	}
}

