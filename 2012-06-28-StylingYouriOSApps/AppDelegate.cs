using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;


namespace Xaminar
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        UITabBarController tabBar;
        LoginDialogViewController loginDvc;
        BaseDialogViewController expenseDvc;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //



        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);


            Util.VisualMode = VisualMode.None;
            //Util.VisualMode = VisualMode.TintColor;
            //Util.VisualMode = VisualMode.UIAppearance;
            AppearanceManager.SetAppearance();

            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
            
            tabBar = new UITabBarController();
            loginDvc = new LoginDialogViewController();


            //have a look at the ExpenseModel class to see why it's hard to do 
            // theming on MonoTouch.Dialog auto-generated stuff
            var expense = new ExpenseModel();
            var context = new BindingContext(null, expense, "Expenses");
            expenseDvc = new BaseDialogViewController(context.Root);
            expenseDvc.Title = "Expenses";
            
            var tabControllers = new UINavigationController[] {
            

                new UINavigationController(loginDvc) {
                    TabBarItem = new UITabBarItem("Login", Resources.Padlock, 1)
                },
                new UINavigationController(expenseDvc) {
                    TabBarItem = new UITabBarItem("Expenses", Resources.Expense, 2)
                }


                /*
                new UINavigationController(deadlines) {
                    TabBarItem = new UITabBarItem("Deadlines", Resources.Deadlines, 2)
                },
                new UINavigationController(reports) {
                    TabBarItem = new UITabBarItem("Reports", Resources.Deadlines, 4)
                },
                new UINavigationController(you) {
                    TabBarItem = new UITabBarItem("You", Resources.You, 3)
                }
                */

                
            };
            
            tabBar.SetViewControllers(tabControllers, false);
            

            window.RootViewController = tabBar;
            window.MakeKeyAndVisible ();
			
            return true;
        }
    }
}

