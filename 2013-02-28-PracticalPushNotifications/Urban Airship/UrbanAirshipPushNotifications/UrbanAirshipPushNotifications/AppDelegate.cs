using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using UrbanAirship;

namespace UrbanAirshipPushNotifications
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UrbanAirshipPushNotificationsViewController viewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new UrbanAirshipPushNotificationsViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			options = new NSDictionary ();
			var takeOffOptions = new NSMutableDictionary ();
			takeOffOptions.SetValueForKey (options, new NSString ("UAirshipTakeOffOptionsLaunchOptionsKey"));

			UAPush.Shared.ResetBadge ();
			UAirship.TakeOff (takeOffOptions);

			return true;
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			UAPush.Shared.RegisterDeviceToken (deviceToken);
		}

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			var message = (NSString) userInfo.ObjectForKey (new NSString ("aps")).ValueForKey(new NSString("alert"));
			
			var alert = new UIAlertView("Notification", message, null, "Okay", null);
			alert.Show ();
		}

		public override void WillTerminate (UIApplication application)
		{
			UAirship.Land ();
		}
	}
}
