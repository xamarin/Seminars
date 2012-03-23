using System;
using System.Threading;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Cloud {
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate {
		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}

		public static bool HasiCloud {get;set;}
		public static bool CheckingForiCloud {get;set;}
		public static NSUrl iCloudUrl {get;set;}
		
		UINavigationController navController;

		UIWindow window;
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			UINavigationBar.Appearance.TintColor = UIColor.FromRGB (102, 204, 255);
			
			window = new UIWindow (UIScreen.MainScreen.Bounds);	
			window.BackgroundColor = UIColor.White;
			window.Bounds = UIScreen.MainScreen.Bounds;
		
			var u = new TaskListScreen();
			navController = new UINavigationController();
			navController.PushViewController (u, false);
			window.AddSubview(navController.View);
            window.MakeKeyAndVisible ();


			new Thread(new ThreadStart(() => {
				CheckingForiCloud = true;
				Console.WriteLine ("??? checking for iCloud");
				var uburl = NSFileManager.DefaultManager.GetUrlForUbiquityContainer(null);
				// or you can specify a container, in this format: "TEAMID.com.xamarin.samples.icloud"
	
				if (uburl == null)
				{
					HasiCloud = false;
					Console.WriteLine ("xxx No iCloud");
				} else {	
					HasiCloud = true;
					iCloudUrl = uburl;
					Console.WriteLine ("yyy " + uburl.AbsoluteUrl);
				}
				CheckingForiCloud = false;

// use app activation notification instead 
//				NSObject o = new NSNumber(HasiCloud);
//				NSDictionary progInfo = NSDictionary.FromObjectAndKey(o , new NSString("HasiCloud"));
//				NSNotificationCenter.DefaultCenter.PostNotificationName("iCloudConnected", o, progInfo);
			
				Console.WriteLine ("___ end of thread");
				
			})).Start();
		
			return true;
		}
	}
}