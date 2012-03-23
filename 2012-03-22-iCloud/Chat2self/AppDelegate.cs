using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

// MAKE SURE YOU:
// * update the bundleid to your own value in Info.plist
// * create a provisioning profile for that bundleid
// * ensure the provisioning profile has iCloud turned on
// * update the TEAMID in the Entitlements.plist
// * update the bundleid in the Entitlements.plist
// * have iCloud enabled on your iOS5 devices
namespace BubbleCell
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		DialogViewController chat;
		UIWindow window;
		
		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			chat = new BubbleViewController();

			window.AddSubview (chat.View);
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}