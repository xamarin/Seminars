using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using System.IO;

namespace XaminarPDFDemo
{
    [Register("PDFView")]
    public class PDFView : UIView
    {
        string _annotatedText;
        int _pageNumber;
        CGPDFDocument _pdf;

        public string AnnotatedText {
            get { return this._annotatedText; }
            set {
                _annotatedText = value;
                this.SetNeedsDisplay ();
            }
        }

        public int PageNumber {
            get { return this._pageNumber; }
            set {
                if (value >= 1 && value <= _pdf.Pages) {
                    _pageNumber = value;
                    this.SetNeedsDisplay ();
                }
            }
        }
        
        public PDFView (IntPtr p) : base(p)
        {
            _pageNumber = 1;
            _pdf = CGPDFDocument.FromFile (Path.Combine (NSBundle.MainBundle.BundlePath, "sample.pdf"));
        }

        public override void Draw (RectangleF rect)
        {
            base.Draw (rect);
            
            CGContext gctx = UIGraphics.GetCurrentContext ();
            gctx.TranslateCTM (0, Bounds.Height);
            gctx.ScaleCTM (1, -1);

            using (CGPDFPage pdfPg = _pdf.GetPage (PageNumber)) {

                gctx.SaveState ();
                      
                RectangleF r = new RectangleF(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height+44);
                CGAffineTransform tf = pdfPg.GetDrawingTransform (CGPDFBox.Crop, r, 0, true);
                gctx.ConcatCTM (tf);
                gctx.DrawPDFPage (pdfPg);
                
                gctx.RestoreState ();
                gctx.TranslateCTM (5, Bounds.Height - 30);
                gctx.SelectFont ("Helvetica", 25f, CGTextEncoding.MacRoman);
                gctx.SetFillColor(UIColor.Red.CGColor);
                gctx.ShowText (AnnotatedText);
            }
        }

        protected override void Dispose (bool disposing)
        {
            _pdf.Dispose ();           
            base.Dispose (disposing);
        }
    }
}
