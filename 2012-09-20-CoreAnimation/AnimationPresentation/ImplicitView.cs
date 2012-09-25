using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace Presentation.AnimationPresentation
{
	[Demo ("View Properties", "Implicit Animations")]
	public class ImplicitView : DemoViewController
	{
		UIView dog;

		public ImplicitView ()
		{
			dog = new UIImageView (UIImage.FromBundle ("Dog.jpg"));
			View.AddSubview (dog);
			Reset ();
		}

		public void Reset ()
		{
			dog.Transform = CGAffineTransform.MakeIdentity ();
			dog.Frame = new RectangleF (100, 100, 200, 230);
			dog.Alpha = 1.0f;
			dog.Center = new PointF (dog.Frame.GetMidX (), dog.Frame.GetMidY ());
			dog.Layer.ShadowOffset = new SizeF (0, 20);
			dog.Layer.ShadowRadius = 10;
			dog.Layer.ShadowColor = new CGColor (0, 0, 0);
			dog.Layer.ShadowOpacity = 1.0f;
			View.BackgroundColor = UIColor.DarkGray;
		}

		public void Center ()
		{			
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dog.Center = new PointF (View.Bounds.GetMidX (), View.Bounds.GetMidY ());
				
			});
		}
		
		public void BackgroundColor ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				View.BackgroundColor = UIColor.FromRGB (0xFF, 0xD3, 0x20);
				
			});
		}

		public void Frame ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {

				dog.Frame = View.Bounds;
			
			});
		}

		public void Alpha ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dog.Alpha = 0.25f;
				
			});
		}

		public void Rotate45Transform ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dog.Transform = CGAffineTransform.MakeRotation ((float)Math.PI/4);
				
			});
		}

		public void RotateN45Transform ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				dog.Transform = CGAffineTransform.MakeRotation ((float)-Math.PI/4);
				
			});
		}

		public void All ()
		{
			UIView.Animate (
				duration: 2,
				animation: () => {
				
				View.BackgroundColor = UIColor.FromRGB (0xFF, 0xD3, 0x20);
				dog.Frame = View.Bounds;
				dog.Alpha = 0.25f;
				dog.Center = new PointF (300, 300);
				dog.Transform = CGAffineTransform.MakeRotation ((float)-Math.PI/4);

			});
		}
	}
}

