using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace MessageBoard.iOS
{
	public class ViewController06 : ViewController05
	{
		RefreshView _refreshView;
		bool _refreshing = false;

		public ViewController06 ()
		{
			Title = "Pretty Refresh";
			SimulateErrors = false;

			_refreshView = new RefreshView (new RectangleF (0, -100, TableView.Frame.Width, 100));
			TableView.AddSubview (_refreshView);

			NavigationItem.LeftBarButtonItem = new UIBarButtonItem (
				UIBarButtonSystemItem.Refresh, delegate {

				StartManualRefresh ();
			}); 
		}

		protected virtual void StartManualRefresh ()
		{
			if (!_refreshing) {

				_refreshing = true;

				_refreshView.Start ();
				UIView.BeginAnimations ("R");
				TableView.ContentInset = new UIEdgeInsets (100, 0, 0, 0);
				TableView.ContentOffset = new PointF (0, -100);
				UIView.CommitAnimations ();

				Refresh ();
			}
		}

		protected virtual void EndManualRefresh ()
		{
			_refreshing = false;

			UIView.BeginAnimations ("R");
			TableView.ContentInset = new UIEdgeInsets (0, 0, 0, 0);
			TableView.ContentOffset = new PointF (0, 0);
			UIView.CommitAnimations ();
			_refreshView.Stop ();
		}

		protected override void ShowError (Exception ex)
		{
			EndManualRefresh ();
			base.ShowError (ex);
		}

		protected override void ShowMessages (System.Collections.Generic.List<Message> messages)
		{
			EndManualRefresh ();
			base.ShowMessages (messages);
		}

		class RefreshView : UIView
		{
			UILabel _label;
			UIActivityIndicatorView _activity;

			public RefreshView (RectangleF frame)
				: base (frame)
			{
				Hidden = true;
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
				_label = new UILabel (new RectangleF (0, 22, frame.Width, 22)) {
					Text = "",
					TextAlignment = UITextAlignment.Center,
				};
				AddSubview (_label);
				_activity = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray) {
					Frame = new RectangleF ((frame.Width - 33) / 2, 55, 33, 33),
				};
				AddSubview (_activity);
			}

			public void Start ()
			{
				_label.Text = "Refreshing at " + DateTime.Now;
				_activity.StartAnimating ();
				Hidden = false;
			}

			public void Stop ()
			{
				_activity.StopAnimating ();
				Hidden = true;
			}
		}
	}
}

