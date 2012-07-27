using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace MessageBoard.iOS
{
	public class ViewController05 : ViewController04
	{
		public ViewController05 ()
		{
			Title = "05 Refresh Button";
			SimulateErrors = false;

			NavigationItem.LeftBarButtonItem = new UIBarButtonItem (
				UIBarButtonSystemItem.Refresh, delegate {

				Refresh ();
			});
		}
	}
}

