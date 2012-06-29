using System;
using MonoTouch.UIKit;

namespace Xaminar
{
    public static class AppearanceManager
    {
        public static void SetAppearance()
        {
            if (Util.UseUIAppearance)
            {
                UINavigationBar.Appearance.SetBackgroundImage(
                  Resources.NavBarHeaderBackground, 
                  UIBarMetrics.Default);
            
                UIBarButtonItem.Appearance.SetBackgroundImage(
                  Resources.BarButtonNormal.CreateResizableImage(new UIEdgeInsets(0,5,0,5)), 
                  UIControlState.Normal, 
                  UIBarMetrics.Default);

                UIBarButtonItem.Appearance.SetBackButtonBackgroundImage(
                  Resources.BarButtonBackNormal.CreateResizableImage(new UIEdgeInsets(0,15,0,5)), 
                  UIControlState.Normal, 
                  UIBarMetrics.Default);
            
                UITabBar.Appearance.BackgroundImage = Resources.TabBarBackground;

                //it'd be nice to be able to do this, but it appears to be one of the bits apple missed
                // so we do it in BaseDialogViewController
                //UITableView.Appearance.BackgroundColor = UIColor.Clear;

                //UIView.Appearance.BackgroundColor = UIColor.FromPatternImage(Resources.Background);

            }
        }
    }
}

