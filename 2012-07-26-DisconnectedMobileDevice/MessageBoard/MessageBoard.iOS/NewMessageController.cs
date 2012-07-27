using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace MessageBoard.iOS
{
	public class NewMessageController : UIViewController
	{
		UITextView _text;

		public event Action<Message> Saved = delegate {};

		public NewMessageController ()
		{
			Title = "New Post";

			NavigationItem.LeftBarButtonItem = new UIBarButtonItem (
				UIBarButtonSystemItem.Cancel,
				delegate {
					DismissModalViewControllerAnimated (true); 
				});

			NavigationItem.RightBarButtonItem = new UIBarButtonItem (
				UIBarButtonSystemItem.Save,
				delegate {
					DismissModalViewControllerAnimated (true);
					Saved (new Message {
						Id = Guid.NewGuid (),
						From = UIDevice.CurrentDevice.Name,
						Text = _text.Text,
						Time = DateTime.UtcNow,
					});
				});

			var b = View.Bounds;
			_text = new UITextView (new RectangleF (0, 0, b.Width, 200)) {
				Font = UIFont.SystemFontOfSize (20),
			};
			_text.BecomeFirstResponder ();
			View.AddSubview (_text);
		}
	}
}

