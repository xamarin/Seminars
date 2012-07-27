using System;
using MonoTouch.UIKit;

namespace MessageBoard.iOS
{
	public class ViewController01 : ViewController00
	{
		protected bool SimulateErrors;

		public ViewController01 ()
		{
			Title = "01 Synchronous";
			SimulateErrors = true;
		}

		protected virtual void Refresh ()
		{
			var client = GetMessageBoardClient ();
			var messages = client.GetMostRecent ();

			ShowMessages (messages);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			Refresh ();
		}

		protected virtual MessageBoardClient GetMessageBoardClient ()
		{
			return new MessageBoardClient (simulateErrors: SimulateErrors);
		}
	}
}

