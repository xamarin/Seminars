using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;

namespace MessageBoard.iOS
{
	public class ViewController00 : UITableViewController
	{
		protected List<Message> _messages;

		public ViewController00 ()
		{
			Title = "00 No Data, Just Display Code";

			TableView.DataSource = new DataSource (this);
		}

		protected virtual void ShowMessages (List<Message> messages)
		{
			_messages = messages;
			TableView.ReloadData ();
		}

		class DataSource : UITableViewDataSource
		{
			ViewController00 _c;

			public DataSource (ViewController00 c) {
				_c = c;
			}
			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override int RowsInSection (UITableView tableView, int section)
			{
				var m = _c._messages;
				return m != null ? m.Count : 0;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var c = tableView.DequeueReusableCell ("M");
				if (c == null) {
					c = new UITableViewCell (UITableViewCellStyle.Subtitle, "M");
					c.TextLabel.Font = UIFont.SystemFontOfSize (16);
				}

				var message = _c._messages [indexPath.Row];

				c.TextLabel.Text = message.Text;

				c.DetailTextLabel.Text = string.Format (
					"{0} @ {1}",
					message.From,
					message.Time.ToLocalTime ());

				return c;
			}
		}
	}
}

