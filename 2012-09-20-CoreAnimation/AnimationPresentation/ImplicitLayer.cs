using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;

namespace Presentation.AnimationPresentation
{
	[Demo ("Layer Properties", "Implicit Animations")]
	public class ImplicitLayer : DemoViewController
	{
		UIView dogView;

		public ImplicitLayer ()
		{
			dogView = new UIImageView (UIImage.FromBundle ("Dog.jpg"));
			View.AddSubview (dogView);
			Reset ();
		}
		
		public void Reset ()
		{
			dogView.Layer.Transform = CATransform3D.Identity;
			dogView.Layer.Frame = new RectangleF (100, 100, 200, 230);
			dogView.Layer.Opacity = 1.0f;
			dogView.Layer.ShadowOffset = new SizeF (0, 20);
			dogView.Layer.ShadowRadius = 10;
			dogView.Layer.ShadowColor = new CGColor (0, 0, 0);
			dogView.Layer.ShadowOpacity = 1.0f;
		}
		
		public void Frame ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dogView.Layer.Frame = View.Bounds;
				
			});
		}
		
		public void Opacity ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dogView.Layer.Opacity = 0.25f;

			});
		}

		public void RotateZ60Transform ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dogView.Layer.Transform = CATransform3D.MakeRotation (
					(float)Math.PI/3, 0, 0, 1);

			});
		}
		
		public void RotateX60Transform ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dogView.Layer.Transform = CATransform3D.MakeRotation (
					(float)Math.PI/3, 1, 0, 0);

			});
		}

		public void RotateY60Transform ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dogView.Layer.Transform = CATransform3D.MakeRotation (
					(float)Math.PI/3, 0, 1, 0);

			});
		}

		public void RotateXYZ60Transform ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dogView.Layer.Transform = CATransform3D.MakeRotation (
					(float)Math.PI/3, 1, 1, 1);

			});
		}
	}
}

