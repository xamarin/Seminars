using System;
using MonoTouch.UIKit;
using System.Threading.Tasks;

namespace MessageBoard.iOS
{
	public class ViewController09 : ViewController08
	{
		public ViewController09 ()
		{
			Title = "09 Post";
			SimulateErrors = true;

			NavigationItem.RightBarButtonItem = new UIBarButtonItem (
				UIBarButtonSystemItem.Compose,
				delegate {

				var c = new NewMessageController ();
				c.Saved += HandleNewMessageSaved;

				PresentModalViewController (new UINavigationController (c), true);			
			});
		}

		protected virtual void HandleNewMessageSaved (Message message)
		{
			GetMessageBoardClient ()
				.PostNewMessageAsync (message)
				.ContinueWith (task => {
					
					if (task.IsFaulted) {
						ShowError (task.Exception);
					}
					else {
						Refresh ();
					}
				
				}, TaskScheduler.FromCurrentSynchronizationContext ());
		}
	}
}

