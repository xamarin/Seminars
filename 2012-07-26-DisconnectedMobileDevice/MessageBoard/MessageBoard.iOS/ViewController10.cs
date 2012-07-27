using System;
using System.Threading.Tasks;
using MonoTouch.UIKit;

namespace MessageBoard.iOS
{
	public class ViewController10 : ViewController09
	{
		OutboxService _outboxService;

		public ViewController10 ()
		{
			Title = "10 Post with Outbox";
			SimulateErrors = false;

			_outboxService = new OutboxService (simulateErrors: SimulateErrors);
			_outboxService.Start ();
		}

		protected virtual void ShowMessage (string title, string message)
		{
			var alert = new UIAlertView (title, message, null, "OK");
			alert.Show ();
		}

		protected override void HandleNewMessageSaved (Message message)
		{
			GetMessageBoardClient ()
				.PostNewMessageAsync (message)
				.ContinueWith (task => {
					 
					if (task.IsFaulted) {
						ShowMessage ("Error", "Your message could not be sent.\n\n" +
							"Please make sure you're connected to the internet.\n\n" +
							"I will keep trying while you do that.");
						_outboxService.AddMessage (message);
					}
					else {
						Refresh ();
					}
				
				}, TaskScheduler.FromCurrentSynchronizationContext ());
		}
	}
}

