using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Presentation.Presenter
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		UISplitViewController split;

		public static AppDelegate Shared {
			get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
		}

		public void SetDetailViewController (UIViewController vc)
		{
			var vcs = split.ViewControllers;
			vcs[1] = vc;
			split.ViewControllers = vcs;
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			var asm = typeof (AnimationPresentation.TransitionsViewController).Assembly;

			split = new UISplitViewController {
				Delegate = new SplitDelegate (),
			};
			split.ViewControllers = new UIViewController[] {
				new UINavigationController (new DemosController (asm)),
				new BlankController (),
			};

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.RootViewController = split;
			window.MakeKeyAndVisible ();
			
			return true;
		}

		class SplitDelegate : UISplitViewControllerDelegate 
		{
			public override bool ShouldHideViewController (UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
			{
				return false;
			}
		}
	}
}

