using System;
using MonoTouch.UIKit;

namespace Xaminar
{

    // NOTE: I'm using resources from the Glyphish icon set in here.
    // Their license says that I can't include the @2x high res ones. So I'm not.
    // However, they are brilliant and cheap as chups (I'm from New Zealand). Go buy them here:
    //
    // http://glyphish.com/
    //
    // $25 gets you 400 normal, @2x, 2 color (grey and white) icons, and a font with them in too.
    // Worth 10x that.
    //
    // The other ones came from AppDesignVault, via the Xamarin Addons library, Foodie theme.
    //
    // The Xamarin logo was shamelessly stolen from their website.


    public static class Resources
    {
        //colors
        public static UIColor NavBarTintColor = UIColor.FromRGBA(115,65,34,255);

        //logos and ui images
        public static UIImage NavBarLogo = UIImage.FromFile("images/navbar-logo.png");
        public static UIImage NavBarHeaderBackground = UIImage.FromFile("images/navbar.png");
        public static UIImage BarButtonNormal = UIImage.FromFile("images/navbar-icon.png");
        public static UIImage BarButtonBackNormal = UIImage.FromFile("images/back-button.png");
        public static UIImage TabBarBackground = UIImage.FromFile("images/tabbar.png");

        //backgrounds
        public static UIImage Background = UIImage.FromFile("images/background.png");

        //icons
        public static UIImage Padlock = UIImage.FromFile("images/padlock-icon.png");
        public static UIImage Expense = UIImage.FromFile("images/expense-icon.png");



    }
}

