using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Cloud {
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate {

		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}

		UIWindow window;
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			var v = new KeyValueViewController();

			window = new UIWindow (UIScreen.MainScreen.Bounds);	
			window.BackgroundColor = UIColor.White;
			window.Bounds = UIScreen.MainScreen.Bounds;
			window.AddSubview(v.View);
            window.MakeKeyAndVisible ();
			return true;
		}
	}
}