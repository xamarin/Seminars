using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Steema.TeeChart;
using MonoTouch.AddressBook;
using MonoTouch.CoreTelephony;

namespace TChartFeatures
{
	public partial class ChartViewController : UIViewController
	{
		public TChart chart= new Steema.TeeChart.TChart();
		SettingsController controller;
		
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public ChartViewController (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public ChartViewController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public ChartViewController () : base("ChartViewController", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
			System.Drawing.RectangleF r = new System.Drawing.RectangleF(0,0,this.View.Bounds.Width,this.View.Bounds.Height-42);						
			chart.Frame = r;
			
			/* AdressBook and Call Center
			ABAddressBook adressBook = new ABAddressBook();
			ABPerson[] contacts = adressBook.GetPeople();
			foreach (ABPerson item in contacts) {
				ABMultiValue<NSDictionary> contact = item.GetPhones();
				foreach (ABMultiValueEntry<NSDictionary> cont in contact) {					
				}
			}
			
			CTCallCenter callCenter = new CTCallCenter();
			CTCall[] calls = callCenter.CurrentCalls;
			foreach (CTCall call in calls) {	
			}
			*/
			
			UIDevice.CurrentDevice.BatteryMonitoringEnabled=true;
			float bLevel = UIDevice.CurrentDevice.BatteryLevel;			
			
			Steema.TeeChart.Styles.NumericGauge series1 = new Steema.TeeChart.Styles.NumericGauge();
			series1.Value = bLevel*100;
			chart.Series.Add(series1);
			series1.Markers[2].AllowEdit=true;
			series1.Markers[2].Text= "Battery Level";
			series1.Markers[1].AllowEdit=true;
			series1.Markers[1].Text= "Percentage";
			
			Steema.TeeChart.Themes.BlackIsBackTheme theme = new Steema.TeeChart.Themes.BlackIsBackTheme(chart.Chart);
			theme.Apply();
			Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(chart.Chart,Steema.TeeChart.Themes.Theme.OnBlackPalette);		
			chart.Aspect.ClipPoints=true;
			chart.Panning.Allow = Steema.TeeChart.ScrollModes.Horizontal;
							
			this.View.AddSubview(chart);	
			UIDevice.CurrentDevice.BatteryMonitoringEnabled=false;

			/*
			// Grab The Context
			UIGraphics.BeginImageContext ( this.View.Frame.Size);
			var ctx = UIGraphics.GetCurrentContext ();
			
			// Render in the context
			this.View.Layer.RenderInContext(ctx);
			 
			// Lets grab a UIImage of the current graphics context
			UIImage i = UIGraphics.GetImageFromCurrentImageContext();
			
			// Set this to your desktop, ie change the martinbowling part
			string png = "/Users/steema/Desktop/chartxx.png";
			// Get the Image as a PNG
			NSData imgData = i.AsPNG();
			NSError err = null;
			if (imgData.Save(png, false, out err))
			{
				Console.WriteLine("saved as " + png);
			} 
			else 
			{
			 	Console.WriteLine("NOT saved as" + png + 
			                    " because" + err.LocalizedDescription);
			}
			
			UIGraphics.EndImageContext ();			
			*/			
		}
		
		#endregion
		
		public override void ViewDidLoad ()
		{
			//base.ViewDidLoad ();
			UIBarButtonItem button= new UIBarButtonItem();
			button.Title = "Settings";
			this.Title="TeeChart";
		
            button.Clicked += delegate(object sender, EventArgs e) {
			
            controller = new SettingsController(chart,this,UITableViewStyle.Grouped);			  
            NavigationController.PushViewController(controller,true);
            };
			
  	        this.NavigationItem.SetRightBarButtonItem(button,true);	
		}
		
		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation) 
		{
			// Refresh Chart rotating the device
			chart.RemoveFromSuperview();
			chart.Frame = View.Frame;
			View.AddSubview(chart);			
			chart.DoInvalidate();			
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}		
	}
}