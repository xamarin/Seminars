using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.CoreFoundation;
using MonoTouch.UIKit;
using ZXing; //DEMO
using ZXing.Mobile; //DEMO

namespace BarcodeScanning.MonoTouch
{
	public class MainViewController : UIViewController
	{
		#region UI Setup
		UIButton buttonScanSample1;
		UIButton buttonScanSample2;
		UIButton buttonScanSample3;

		void BuildUI()
		{
			var bounds = this.View.Bounds;
			
			buttonScanSample1 = new UIButton(UIButtonType.RoundedRect)
			{
				Frame = new RectangleF(10, 10, bounds.Width - 20, 40)
			};
			buttonScanSample1.SetTitle("Sample 1", UIControlState.Normal);
			this.View.Add(buttonScanSample1);
						
			this.buttonScanSample1.TouchUpInside += Handle_buttonScanSample1_Click;
		}

		public void ShowMessage(string msg)
		{
			this.BeginInvokeOnMainThread(() => {
				var av = new UIAlertView("Barcode", msg, null, "OK", null);
				av.Show();
			});
		}

		public MainViewController () : base()
		{
		}

		public override void ViewDidLoad ()
		{
			BuildUI();
		}
		#endregion

		void Handle_buttonScanSample1_Click(object sender, EventArgs e)
		{
			var scanner = new MobileBarcodeScanner();

			scanner.UseCustomOverlay = true;

			//var result = await scanner.Scan();
			scanner.Scan().ContinueWith((t) => 
			{
				ShowMessage(t.Result != null ? 
				                  t.Result.Text : "Canceled / No Barcode");
			});
		}
	
	}
}

