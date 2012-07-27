using System;
using MonoTouch.Foundation;

namespace MessageBoard.iOS
{
	public class ViewController07 : ViewController06
	{
		NSTimer _autoRefreshTimer;
		bool _autoRefreshing = false;

		public ViewController07 ()
		{
			Title = "Auto Refresh";
			SimulateErrors = true;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			_autoRefreshTimer = NSTimer.CreateRepeatingScheduledTimer (
				MessageBoardClient.RefreshInterval.TotalSeconds,
				delegate {

				Refresh ();
				_autoRefreshing = true;
			});
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			_autoRefreshTimer.Invalidate ();
			_autoRefreshTimer = null;
		}

		protected override void ShowError (Exception ex)
		{
			if (!_autoRefreshing) {
				base.ShowError (ex);
			}
			_autoRefreshing = false;
		}

		protected override void ShowMessages (System.Collections.Generic.List<Message> messages)
		{
			base.ShowMessages (messages);
			_autoRefreshing = false;
		}
	}
}

