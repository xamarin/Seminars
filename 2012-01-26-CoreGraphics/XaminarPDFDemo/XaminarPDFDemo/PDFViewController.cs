using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;

namespace XaminarPDFDemo
{
    public partial class PDFViewController : UIViewController
    {
        string _text;
        
        public PDFViewController (string text) : base ("PDFViewController", null)
        {
            _text = text;
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
            
            // previous page
            pdfToolbar.Items[2].Clicked += delegate {
                
                PDFView pdfV = View as PDFView;
                pdfV.PageNumber--;
            };
            
            // next page
            pdfToolbar.Items[3].Clicked += delegate {
                PDFView pdfView = View as PDFView;
                pdfView.PageNumber++;
            };
            
            // close
            pdfToolbar.Items[0].Clicked += delegate {
                this.DismissModalViewControllerAnimated (true);
            };
        }

        public override void LoadView ()
        {
            base.LoadView ();
            
            PDFView pv = (View as PDFView);
            
            if (pv != null)
                pv.AnnotatedText = _text;
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

