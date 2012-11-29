using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using ZXing;
using ZXing.Mobile;

namespace BarcodeScanning.MonoAndroid
{
	[Activity (Label = "Barcode Scanning", MainLauncher = true)]
	public class Activity1 : Activity
	{
		#region UI Code
		Button buttonSample1;
		Button buttonSample2;
		Button buttonSample3;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			buttonSample1 = FindViewById<Button> (Resource.Id.buttonSample1);
			buttonSample2 = FindViewById<Button> (Resource.Id.buttonSample2);
			buttonSample3 = FindViewById<Button> (Resource.Id.buttonSample3);
			
			buttonSample1.Click += buttonSample1_Click;
			buttonSample2.Click += buttonSample2_Click;
			buttonSample3.Click += buttonSample3_Click;
		}

		void ShowMessage(string msg)
		{
			this.RunOnUiThread(() => {

				AlertDialog ad = null;

				var adBuilder = new AlertDialog.Builder(this);
				adBuilder.SetTitle("Barcode");
				adBuilder.SetMessage(msg);
				adBuilder.SetNegativeButton("OK",(s, e) => {
					ad.Dismiss();
					ad.Cancel();
				});
				ad = adBuilder.Create();
				ad.Show();

			});
		}
		#endregion

		void buttonSample1_Click (object sender, EventArgs e)
		{
			var scanner = new MobileBarcodeScanner(this);
			scanner.Scan().ContinueWith((t) => {
				ShowMessage(t.Result != null ? "Scanned: " + t.Result.Text : "No Barcode Scanned");
			});
		}

		void buttonSample2_Click (object sender, EventArgs e)
		{
			var scanner = new MobileBarcodeScanner(this);
			var options = new MobileBarcodeScanningOptions()
			{
				PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.All_1D },

			};

			scanner.TopText = "Hold the barcode close to the camera";
			scanner.BottomText = "Barcode will scan automatigically";

			scanner.Scan(options).ContinueWith((t) => {
				ShowMessage(t.Result != null ? "Scanned: " + t.Result.Text : "No Barcode Scanned");
			});
		}

		void buttonSample3_Click (object sender, EventArgs e)
		{
			var scanner = new MobileBarcodeScanner(this);

			var overlay = (LinearLayout)this.LayoutInflater.Inflate(Resource.Layout.ScanOverlayLayout, null, false);

			var buttonCancel = overlay.FindViewById<Button>(Resource.Id.buttonCancelScan);
			var buttonFlash = overlay.FindViewById<Button>(Resource.Id.buttonToggleFlash);

			buttonCancel.Click += (object sender2, EventArgs e2) => 
			{
				scanner.Cancel();
			};

			buttonFlash.Click += (object sender2, EventArgs e2) => 
			{
				scanner.ToggleTorch();
			};

			scanner.UseCustomOverlay = true;
			scanner.CustomOverlay = overlay;

			scanner.Scan().ContinueWith((t) => {
				ShowMessage(t.Result != null ? "Scanned: " + t.Result.Text : "No Barcode Scanned");
			});
		}
	}
}



