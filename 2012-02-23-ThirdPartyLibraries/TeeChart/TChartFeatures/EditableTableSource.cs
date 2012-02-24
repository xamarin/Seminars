using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class EditableTableSource : UITableViewSource
	{
		private static List<string> _items;
		private string _cellId;
	
		public bool Editing { get; set; }
		
		static EditableTableSource()
		{
			// Use a static list for these demos to demonstrate persistence, in reality
			// this would update a SQlite database.
			_items = new List<string>()
			{
				"Vitamin A",
				"Niacin",
				"Vitamin B12",
				"Vitamin K",
				"Omega 3"
			};	
		}
		
		public EditableTableSource()
		{
			_cellId = "cellid";
		}
		
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return "Pills";
		}
		
		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
			    _items.RemoveAt(indexPath.Row);
			    tableView.DeleteRows(new [] { indexPath }, UITableViewRowAnimation.Fade);
			}
		}
		
		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}
		
		public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
		{
			// This is a basic example, with a database it'll be more tricky
			string item = _items[sourceIndexPath.Row];
			_items.RemoveAt(sourceIndexPath.Row);
			_items.Insert(destinationIndexPath.Row,item);
		}
	
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _items.Count;
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(_cellId); 
			
			if (cell == null )
					cell = new UITableViewCell(UITableViewCellStyle.Default, _cellId);
			
			cell.TextLabel.Text = _items[indexPath.Row];
			cell.ShouldIndentWhileEditing = true;
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			    
			return cell; 
		}
	}
}
