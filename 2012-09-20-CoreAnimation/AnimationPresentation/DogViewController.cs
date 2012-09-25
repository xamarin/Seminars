using System;
using MonoTouch.UIKit;

namespace Presentation.AnimationPresentation
{
	class DogViewController : UIViewController
	{
		public DogViewController ()
		{
			var iv = new UIImageView (UIImage.FromBundle ("Dog.jpg"));
			iv.UserInteractionEnabled = true;
			iv.AddGestureRecognizer (new UITapGestureRecognizer (g => {
				DismissViewController (true, null);
			}));
			View = iv;
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

