// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace HelloWorld
{
	[Register ("HelloWorldViewController")]
	partial class HelloWorldViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel MyLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton MyButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MyLabel != null) {
				MyLabel.Dispose ();
				MyLabel = null;
			}

			if (MyButton != null) {
				MyButton.Dispose ();
				MyButton = null;
			}
		}
	}
}
