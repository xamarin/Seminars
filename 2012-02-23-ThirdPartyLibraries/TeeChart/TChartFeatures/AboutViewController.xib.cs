
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace TChartFeatures
{
	public partial class AboutViewController : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public AboutViewController (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public AboutViewController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AboutViewController () : base("AboutViewController", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
			  this.View.BackgroundColor = UIColor.Black;
			 
 			  float ix = (this.View.Bounds.Right /2) - 55.5f;

			  UIGlassButton buttonSite = new UIGlassButton(new RectangleF((this.View.Bounds.Right /2)-(105), 275f,220f,25f));
		 	  buttonSite.SetTitle("http://www.steema.com", UIControlState.Normal);
			  buttonSite.SetTitleColor(UIColor.White, UIControlState.Normal);
			  buttonSite.SetValueForKey(UIColor.Black, "tintColor");
			  this.View.AddSubview(buttonSite);
			
		        buttonSite.TouchDown += delegate { // trigger action
		            NSUrl url = new NSUrl("http://www.steema.com");
		            if (!UIApplication.SharedApplication.OpenUrl(url))
		            {
		                var av = new UIAlertView("Not supported"
		                    , "Scheme 'tel:' is not supported on this device"
		                    , null
		                    , "Ok thanks"
		                    , null);
		                av.Show();
		            }
		        };			
						
			  var labelRect = new RectangleF((this.View.Bounds.Right /2)-90, 300f,200f,25f);
			    using (var label = new UILabel(labelRect))
			    {
			     label.TextColor = UIColor.White;
				 label.BackgroundColor = UIColor.Black;
				 label.AdjustsFontSizeToFitWidth=true;
			     label.Text = "2011 CopyRight by Steema Software, S.L.";
			     this.View.AddSubview(label);
			    }
			
			  var imageRect = new RectangleF(ix-9, 30f, 127f, 80f);
				using (var myImage = new UIImageView(imageRect))
				{  
				    myImage.Image = UIImage.FromFile("images/SteemaWhite127x80.png");
				    myImage.Opaque = false;
				
				    this.View.AddSubview(myImage);
				}
			
				var image2Rect = new RectangleF(ix, 140f, 111f, 80f);
				using (var logo = new UIImageView(image2Rect))
				{  
				    logo.Image = UIImage.FromFile("images/TeeChartNETForIPhone111x80.png");
				    logo.Opaque = false;
				
				    this.View.AddSubview(logo);
				}
		}
		
		#endregion
	}
}

