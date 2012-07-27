using System;
using MonoTouch.UIKit;

namespace MessageBoard.iOS
{
	public class ViewController02 : ViewController01
	{
		public ViewController02 ()
		{
			Title = "02 Error Handling";
			SimulateErrors = true;
		}

		protected virtual void ShowError (Exception ex)
		{
			var alert = new UIAlertView ("Error", ex.Message, null, "OK");
			alert.Show ();
		}

		protected override void Refresh ()
		{
			try {

				var client = GetMessageBoardClient ();
				ShowMessages (client.GetMostRecent ());
				
			} catch (Exception ex) {

				ShowError (ex);

			}
		}
	}
}

