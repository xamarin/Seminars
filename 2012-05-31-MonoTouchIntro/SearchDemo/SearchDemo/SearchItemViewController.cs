using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SearchDemo
{
    public partial class SearchItemViewController : UIViewController
    {
        Bing bing;

        public SearchItem Item { get; set; }

        public SearchItemViewController () : base ("SearchItemViewController", null)
        {
            bing = new Bing ();
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            webView.LoadStarted += (sender, e) => {
                speakButton.Enabled = false;
                UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
            };

            webView.LoadFinished += (sender, e) => {
                UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
                speakButton.Enabled = true;
            };

            webView.LoadRequest (new NSUrlRequest (new NSUrl (Item.Url)));

            speakButton.Clicked += (sender, e) => {
                bing.Speak (Item.Title, "en");
            };
        }
        
        public override void ViewDidUnload ()
        {
            base.ViewDidUnload ();
            
            ReleaseDesignerOutlets ();
        }

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }
        
        public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
        }
    }
}

