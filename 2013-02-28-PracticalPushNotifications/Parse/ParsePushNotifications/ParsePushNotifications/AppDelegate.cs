using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Parse;

namespace ParsePushNotifications
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		ParsePushNotificationsViewController viewController;

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
			
			viewController = new ParsePushNotificationsViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			// App Id, Client Key
			ParseService.SetAppId ("nDjemh2ScZ5ivURdh2ro5gyryBu8CjvrdNNYHdS5", "gdZctpnb4j1HZ19ayHfHiKhAVDcNVhXM97FjOCpa");

			app.RegisterForRemoteNotificationTypes (UIRemoteNotificationType.Alert 
			                                        | UIRemoteNotificationType.Badge 
			                                        | UIRemoteNotificationType.Sound);
			return true;
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			ParsePush.StoreDeviceToken (deviceToken);
			ParsePush.SubscribeToChannelAsync ("");
		}

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			ParsePush.HandlePush (userInfo);
		}
	}
}

