using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MessageUI;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace XaminarPDFDemo
{
    public partial class SendPDFController : UIViewController
    {
        MFMailComposeViewController _mail;
        PDFViewController _pdf;
        
        public SendPDFController () : base ("SendPDFController", null)
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
            
            tv.BecomeFirstResponder ();
            
            toolbar.Items [0].Clicked += (o, s) =>
            {
                string text = tv.Text;
                NSData pdfData = CreatePDF (text, 500f, 700f);
                
                if (MFMailComposeViewController.CanSendMail) {
                    _mail = new MFMailComposeViewController ();
                    _mail.SetMessageBody (tv.Text, false);
                    _mail.AddAttachmentData (pdfData, "text/x-pdf", "test.pdf");
                    _mail.Finished += HandleMailFinished;
                    this.PresentModalViewController (_mail, true);
                } else {
                    UIAlertView alert = new UIAlertView ("App", "Could not send mail.", null, "OK", null);
                    alert.Show ();
                }
            };
            
            toolbar.Items [1].Clicked += (o, s) =>
            {
                _pdf = new PDFViewController (tv.Text);
                this.PresentModalViewController (_pdf, true);
            };
        }
        
        NSData CreatePDF (string text, float w, float h)
        {
            NSMutableData data = new NSMutableData ();
            UIGraphics.BeginPDFContext (data, new RectangleF (0, 0, w, h), null);
            
            UIGraphics.BeginPDFPage ();       
            CGContext gctx = UIGraphics.GetCurrentContext ();  
            gctx.ScaleCTM (1, -1);
            gctx.TranslateCTM (0, -25f);      
            gctx.SelectFont ("Helvetica", 25f, CGTextEncoding.MacRoman);
            gctx.ShowText (text);
            
            UIGraphics.EndPDFContent ();
            return data;
        }

        void HandleMailFinished (object sender, MFComposeResultEventArgs e)
        {
            if (e.Result == MFMailComposeResult.Sent) {
                UIAlertView alert = new UIAlertView ("App", "Pdf file attached to outgoing mail.", null, "OK", null);
                alert.Show ();
            } else if (e.Result == MFMailComposeResult.Failed) {
                UIAlertView alert = new UIAlertView ("App", "Could not send mail.", null, "OK", null);
                alert.Show ();
            }
            
            e.Controller.DismissModalViewControllerAnimated (true);
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

