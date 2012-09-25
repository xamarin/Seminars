using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace Presentation.AnimationPresentation
{
	[Demo ("View", "Transitions")]
	public class TransitionsView : DemoViewController
	{
		UIView dogView;
		UIView catView;
		
		public TransitionsView ()
		{
			dogView = new UIImageView (UIImage.FromBundle ("Dog.jpg")) {
				Frame = new RectangleF (22, 22, 660, 710),
			};
			View.AddSubview (dogView);

			catView = new UIImageView (UIImage.FromBundle ("Cat.jpg")) {
				Frame = dogView.Frame,
			};

			Reset ();
		}
		
		public void Reset ()
		{
			View.AddSubview (dogView);
			catView.Frame = dogView.Frame;
		}

		bool IsDogVisible { get { return dogView.Superview != null; } }
		
		public void CurlDown ()
		{
			UIView.Transition (
				fromView: IsDogVisible ? dogView : catView,
				toView: IsDogVisible ? catView : dogView,
				duration: 3,
				options: UIViewAnimationOptions.TransitionCurlDown,
				completion: () => {});
		}

		public void CurlUp ()
		{
			UIView.Transition (
				fromView: IsDogVisible ? dogView : catView,
				toView: IsDogVisible ? catView : dogView,
				duration: 3,
				options: UIViewAnimationOptions.TransitionCurlUp,
				completion: () => {});
		}

		public void FlipFromBottom ()
		{
			UIView.Transition (
				fromView: IsDogVisible ? dogView : catView,
				toView: IsDogVisible ? catView : dogView,
				duration: 3,
				options: UIViewAnimationOptions.TransitionFlipFromBottom,
				completion: () => {});
		}

		public void FlipFromTop ()
		{
			UIView.Transition (
				fromView: IsDogVisible ? dogView : catView,
				toView: IsDogVisible ? catView : dogView,
				duration: 3,
				options: UIViewAnimationOptions.TransitionFlipFromTop,
				completion: () => {});
		}

		public void FlipFromLeft ()
		{
			UIView.Transition (
				fromView: IsDogVisible ? dogView : catView,
				toView: IsDogVisible ? catView : dogView,
				duration: 3,
				options: UIViewAnimationOptions.TransitionFlipFromLeft,
				completion: () => {});
		}

		public void FlipFromRight ()
		{
			UIView.Transition (
				fromView: IsDogVisible ? dogView : catView,
				toView: IsDogVisible ? catView : dogView,
				duration: 3,
				options: UIViewAnimationOptions.TransitionFlipFromRight,
				completion: () => {});
		}
		
		public void CrossDisolve ()
		{
			UIView.Transition (
				fromView: IsDogVisible ? dogView : catView,
				toView: IsDogVisible ? catView : dogView,
				duration: 3,
				options: UIViewAnimationOptions.TransitionCrossDissolve,
				completion: () => {});
		}
		
	}
}

