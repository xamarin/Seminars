using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Presentation.Presenter
{
	public class CommandsController : UITableViewController
	{
		UIViewController vc;

		class Command
		{
			public MethodInfo Method;
		}

		List<Command> commands;

		public CommandsController (Demo demo, UIViewController vc)
		{
			this.vc = vc;

			var type = vc.GetType ();

			Title = demo.Name;

			var q = from m in type.GetMethods (BindingFlags.Instance | BindingFlags.Public)
					where m.DeclaringType == type &&
						m.GetParameters ().Length == 0
					orderby m.Name
					select new Command { Method = m };
			commands = q.ToList ();

			TableView.Delegate = new CommandsDelegate (this);
			TableView.DataSource = new CommandsDataSource (this);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		void PresentCommand (Command command)
		{
			var sel = TableView.IndexPathForSelectedRow;
			if (sel != null) {
				TableView.DeselectRow (sel, true);
			}
			try {
				command.Method.Invoke (vc, null);
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}
		}

		class CommandsDelegate : UITableViewDelegate
		{
			CommandsController controller;
			public CommandsDelegate (CommandsController controller)
			{
				this.controller = controller;
			}
			
			public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var command = controller.commands[indexPath.Row];
				controller.PresentCommand (command);
			}
		}
		
		class CommandsDataSource : UITableViewDataSource
		{
			CommandsController controller;
			public CommandsDataSource (CommandsController controller)
			{
				this.controller = controller;
			}
			
			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}
			
			public override int RowsInSection (UITableView tableView, int section)
			{
				return controller.commands.Count;
			}
			
			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell ("C");
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, "C");
				}
				
				var command = controller.commands[indexPath.Row];
				cell.TextLabel.Text = command.Method.Name;
				
				return cell;
			}
		}
	}
}

