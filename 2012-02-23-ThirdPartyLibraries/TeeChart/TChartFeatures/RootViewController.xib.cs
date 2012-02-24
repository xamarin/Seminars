
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Steema.TeeChart;
using System.Drawing;


namespace TChartFeatures
{
	public partial class RootViewController : UIViewController
	{
		//public TChart chart= new Steema.TeeChart.TChart();
		//SettingsController controller;
		//ChartViewController controller;
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			  View.BackgroundColor=UIColor.Black;
			  NavigationController.NavigationBar.TintColor = UIColor.Black;
			  this.NavigationItem.RightBarButtonItem=null;
 
			  UIColor tint = UIColor.Black;
		 	  UIGlassButton button = new UIGlassButton(new RectangleF(20, 265, 280, 48));
		 	  button.SetTitle("Chart Features", UIControlState.Normal);
			  button.SetTitleColor(UIColor.White, UIControlState.Normal);
			  button.SetValueForKey(tint, "tintColor");
			  this.View.AddSubview(button);
			
     	      button.TouchDown += delegate(object sender, EventArgs e) {
				NavigationController.SetNavigationBarHidden(false,true);
               // controller = new ChartViewController();	
//				SeriesTypesController scontroller = new SeriesTypesController(controller.chart,controller,UITableViewStyle.Grouped); 		  
				SeriesTypesController scontroller = new SeriesTypesController(UITableViewStyle.Grouped); 		  
                NavigationController.PushViewController(scontroller,true);
              };

		 	  UIGlassButton buttonStatistical = new UIGlassButton(new RectangleF(20, 315, 280, 48));
		 	  buttonStatistical.SetTitle("Battery Level Demo", UIControlState.Normal);
			  buttonStatistical.SetTitleColor(UIColor.White, UIControlState.Normal);
			  buttonStatistical.SetValueForKey(tint, "tintColor");
			  this.View.AddSubview(buttonStatistical);
			
     	      buttonStatistical.TouchDown += delegate(object sender, EventArgs e) {
                ChartViewController controller = new ChartViewController();			  
                NavigationController.PushViewController(controller,true);
              };

			  UIGlassButton buttonAbout = new UIGlassButton(new RectangleF(20, 365, 280, 48));
		 	  buttonAbout.SetTitle("About", UIControlState.Normal);
			  buttonAbout.SetTitleColor(UIColor.White, UIControlState.Normal);
			  buttonAbout.SetValueForKey(tint, "tintColor");
			  this.View.AddSubview(buttonAbout);
			
     	      buttonAbout.TouchDown += delegate(object sender, EventArgs e) {
                AboutViewController controller = new AboutViewController();			  
                NavigationController.PushViewController(controller,true);
              };
			
			  //float ix = (this.View.Bounds.Right /2) - 55.5f;
			
			  var image2Rect = new RectangleF(0f, 20f, 234f, 225f);
				using (var myImage = new UIImageView(image2Rect))
				{  
				    myImage.Image = UIImage.FromFile("images/Chart1.png");
				    myImage.Opaque = false;
				
				    this.View.AddSubview(myImage);
				}

			  var imageRect = new RectangleF(190f, 10f, 111f, 80f);
				using (var myImage = new UIImageView(imageRect))
				{  
				    myImage.Image = UIImage.FromFile("images/TeeChartNETForIPhone111x80.png");
				    myImage.Opaque = false;
				
				    this.View.AddSubview(myImage);
				}
		}
		
		/*public override void ViewDidAppear (bool animated)  
		{  
		    UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications();  
		}  
   
		public override void ViewWillDisappear (bool animated)  
		{  
     		UIDevice.CurrentDevice.EndGeneratingDeviceOrientationNotifications();  
		}  
   
	 	private void DeviceRotated(NSNotification notification)
		{  
        	// change the views on demand  		
 		}
 		*/
		
		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation) 
		{
			Console.Write("ss");
		}
		
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	
		public RootViewController (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public RootViewController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public RootViewController () : base("RootViewController", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
//			System.Drawing.RectangleF r = new System.Drawing.RectangleF(0,0,this.View.Bounds.Width,this.View.Bounds.Height-42);						
//			chart.Frame = r;
//			
//			Steema.TeeChart.Styles.Bar series1 = new Steema.TeeChart.Styles.Bar();
//			series1.FillSampleValues();
//			series1.ColorEachPoint = true;
//			chart.Series.Add(series1);
//			
//			Steema.TeeChart.Themes.BlackIsBackTheme theme = new Steema.TeeChart.Themes.BlackIsBackTheme(chart.Chart);
//			theme.Apply();
//			Steema.TeeChart.Themes.ColorPalettes.ApplyPalette(chart.Chart,Steema.TeeChart.Themes.Theme.OnBlackPalette);		
//			chart.Panel.Color = UIColor.DarkGray.CGColor;
//			chart.Walls.Back.Color = UIColor.DarkGray.CGColor;			
//			
//			this.View.AddSubview(chart);	
			// do not uncomment .....--- for rotate event NSObject notificationObserver  = NSNotificationCenter.DefaultCenter.AddObserver("UIDeviceOrientationDidChangeNotification", DeviceRotated );

		}
		#endregion
	}
}

