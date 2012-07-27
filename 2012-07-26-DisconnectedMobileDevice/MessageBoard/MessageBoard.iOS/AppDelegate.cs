using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MessageBoard.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow _window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			var home = new ViewController10 ();

			_window = new UIWindow (UIScreen.MainScreen.Bounds);
			_window.RootViewController = new UINavigationController (home);
			_window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

