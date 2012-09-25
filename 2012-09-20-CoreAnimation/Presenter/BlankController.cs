using System;
using MonoTouch.UIKit;

namespace Presentation.Presenter
{
	public class BlankController : UIViewController
	{
		public BlankController ()
		{
			Title = "Blank";
			View.BackgroundColor = UIColor.DarkGray;
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

