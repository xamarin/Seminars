using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;



namespace Xaminar
{
    // Base class to do a load of setup and configuration for each dialogviewcontroller we use.

    public class BaseDialogViewController : DialogViewController
    {
        public BaseDialogViewController() : base(null, false)
        {
        }

        public BaseDialogViewController(bool pushing) : base(null, pushing)
        {

        }

        public BaseDialogViewController(RootElement root) : base(root)
        {

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ParentViewController.View.BackgroundColor = UIColor.FromPatternImage(Resources.Background);
            View.BackgroundColor = UIColor.Clear;

            if (Util.UseTintColor)
            {
                if (NavigationController != null)
                {
                    NavigationController.NavigationBar.TintColor = Resources.NavBarTintColor;
                }
            }

            //the first and last ones are to keep the pushed stack with the correct info on it,
            // and the tab bar
            if (NavigationItem != null)
            {
                NavigationItem.Title = Title;
                NavigationItem.TitleView = new UIImageView(Resources.NavBarLogo);
            }
            if (TabBarItem != null)
            {
                TabBarItem.Title = Title;
            }
        }

    }
}

        