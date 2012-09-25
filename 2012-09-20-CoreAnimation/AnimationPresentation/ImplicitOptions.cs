using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace Presentation.AnimationPresentation
{
	[Demo ("Options", "Implicit Animations")]
	public class ImplicitOptions : DemoViewController
	{
		UIView dog;
		
		public ImplicitOptions ()
		{
			dog = new UIImageView (UIImage.FromBundle ("Dog.jpg"));
			View.AddSubview (dog);
			Reset ();
		}
		
		public void Reset ()
		{
			dog.Frame = new RectangleF (100, 100, 200, 230);
		}
		
		public void Autoreverse ()
		{
			UIView.Animate (
				duration: 2,
				delay: 0,
				options: UIViewAnimationOptions.Autoreverse,
				animation: () => {
				
				dog.Center = new PointF (View.Bounds.GetMidX (), View.Bounds.GetMidY ());

			},
			completion: () => {});
		}
		
		public void AutoreverseWithReset ()
		{
			UIView.Animate (
				duration: 2,
				delay: 0,
				options: UIViewAnimationOptions.Autoreverse,
				animation: () => {
				
				dog.Center = new PointF (View.Bounds.GetMidX (), View.Bounds.GetMidY ());

			},
			completion: Reset);
		}
		
		public void RepeatWithAutoreverse ()
		{
			UIView.Animate (
				duration: 2,
				delay: 0,
				options: UIViewAnimationOptions.Repeat | UIViewAnimationOptions.Autoreverse,
				animation: () => {
				
				dog.Center = new PointF (View.Bounds.GetMidX (), View.Bounds.GetMidY ());

			},
			completion: () => {});
		}

		public void Repeat ()
		{
			UIView.Animate (
				duration: 2,
				delay: 0,
				options: UIViewAnimationOptions.Repeat,
				animation: () => {
				
				dog.Center = new PointF (View.Bounds.GetMidX (), View.Bounds.GetMidY ());
				
			},
			completion: () => {});
		}

		public void Linear ()
		{
			UIView.Animate (
				duration: 2,
				delay: 0,
				options: UIViewAnimationOptions.CurveLinear | UIViewAnimationOptions.Repeat | UIViewAnimationOptions.Autoreverse,
				animation: () => {
				
				dog.Center = new PointF (View.Bounds.GetMidX (), View.Bounds.GetMidY ());
				
			},
			completion: () => {});
		}
	}
}

