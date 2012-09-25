using System;
using MonoTouch.UIKit;

namespace Presentation
{
	public abstract class DemoViewController : UIViewController
	{
		public DemoViewController ()
		{
			View.BackgroundColor = UIColor.DarkGray;
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

