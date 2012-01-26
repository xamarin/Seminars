using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;

namespace XaminarCGDemo
{
    public partial class XaminarCGDemoViewController : UIViewController
    {
        StarView _star;
        
        public XaminarCGDemoViewController () : base ("XaminarCGDemoViewController", null)
        {
        }
        
        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            //any additional setup after loading the view, typically from a nib.
            
            _star = new StarView (){Frame = View.Frame};
            View.AddSubview(_star);
        }
        
        public override void ViewDidUnload ()
        {
            base.ViewDidUnload ();
            
            // Release any retained subviews of the main view.
            // e.g. myOutlet = null;
        }
        
        public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
        }
    }
}
