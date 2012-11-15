using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.Xna.Framework;

namespace ChaseCameraSample
{
	[Activity (Label = "ChaseCamera.Android", 
	           MainLauncher = true,
	           Icon = "@drawable/icon",
	           Theme = "@style/Theme.Splash",
                AlwaysRetainTaskState=true,
	           LaunchMode=LaunchMode.SingleInstance,
	           ConfigurationChanges = ConfigChanges.Orientation | 
			ConfigChanges.KeyboardHidden | 
			ConfigChanges.Keyboard)]
	public class Activity1 : AndroidGameActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create our OpenGL view, and display it
			ChaseCameraGame.Activity = this;
			var g = new ChaseCameraGame ();
			SetContentView (g.Window);
			g.Run ();
		}
		
	}
}


